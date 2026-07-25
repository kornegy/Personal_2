#!/usr/bin/env bash
#
# Делает сертификат разработки ASP.NET Core доверенным на Linux.
# После этого https://localhost:7150 открывается с замком, без предупреждения.
#
# Зачем скрипт: на Linux команда «dotnet dev-certs https --trust» доводит дело
# до конца не везде. Системному хранилищу она сертификат не отдаёт, а Chrome
# и Firefox системному хранилищу и так не верят — у них свои базы (NSS).
# Скрипт закрывает все три места сразу.
#
# Запуск:  bash scripts/trust-dev-cert.sh
#
# На продакшене это не нужно: там сертификат выдаёт хостинг или Let's Encrypt.

set -euo pipefail

CERT_NAME="ASP.NET Core dev cert (localhost)"
CERT_FILE="$(mktemp --suffix=.crt)"
SYSTEM_CERT="/usr/local/share/ca-certificates/aspnet-core-dev.crt"

cleanup() { rm -f "$CERT_FILE"; }
trap cleanup EXIT

echo "==> Проверяю сертификат разработки"
if ! command -v dotnet >/dev/null; then
    echo "Не найден dotnet. Установите .NET SDK 8 и повторите." >&2
    exit 1
fi

# Создаст сертификат, если его ещё нет, и ничего не сделает, если он есть.
dotnet dev-certs https >/dev/null
dotnet dev-certs https --export-path "$CERT_FILE" --format PEM >/dev/null
echo "    сертификат выгружен"

echo "==> Системное хранилище (нужно для curl и самого dotnet)"
if command -v update-ca-certificates >/dev/null; then
    sudo cp "$CERT_FILE" "$SYSTEM_CERT"
    sudo update-ca-certificates >/dev/null
    echo "    добавлен в $SYSTEM_CERT"
else
    echo "    пропущено: update-ca-certificates не найден (не Debian/Ubuntu)"
fi

if ! command -v certutil >/dev/null; then
    echo
    echo "Для браузеров нужен certutil. Установите и запустите скрипт ещё раз:"
    echo "    sudo apt install libnss3-tools"
    exit 0
fi

# Добавляет сертификат в базу NSS. Флаг P — «доверять этому серверу»:
# сертификат выписан на localhost и удостоверяющим центром не является.
add_to_nss() {
    local db="$1"
    certutil -d "$db" -D -n "$CERT_NAME" 2>/dev/null || true   # убрать старую запись
    certutil -d "$db" -A -t "P,," -n "$CERT_NAME" -i "$CERT_FILE"
}

echo "==> Chrome, Chromium, Edge"
CHROME_DB="$HOME/.pki/nssdb"
if [ ! -f "$CHROME_DB/cert9.db" ]; then
    # База появляется после первого запуска браузера, но её можно создать и самим.
    mkdir -p "$CHROME_DB"
    certutil -d "sql:$CHROME_DB" -N --empty-password
    echo "    база создана"
fi
add_to_nss "sql:$CHROME_DB"
echo "    добавлен в $CHROME_DB"

echo "==> Firefox"
firefox_found=0
for profile in "$HOME"/.mozilla/firefox/*/ "$HOME"/snap/firefox/common/.mozilla/firefox/*/; do
    [ -f "$profile/cert9.db" ] || continue
    add_to_nss "sql:$profile"
    echo "    добавлен в $(basename "$profile")"
    firefox_found=1
done
[ "$firefox_found" -eq 0 ] && echo "    профили не найдены, пропускаю"

echo
echo "Готово. Закройте браузер полностью и откройте заново —"
echo "https://localhost:7150 должен открыться с замком."
