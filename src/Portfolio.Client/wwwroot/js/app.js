// Небольшие браузерные операции, недоступные из C#: хранилище, язык браузера, атрибуты документа.
// Отдельный файл вместо инлайн-скрипта — так Content-Security-Policy остаётся строгой.
window.portfolioApp = {
    storageKey: 'portfolio-language',

    getStoredLanguage: function () {
        try {
            return localStorage.getItem(window.portfolioApp.storageKey);
        } catch {
            return null; // приватный режим — просто работаем без сохранения
        }
    },

    storeLanguage: function (code) {
        try {
            localStorage.setItem(window.portfolioApp.storageKey, code);
        } catch {
            // сохранять некуда, это не ошибка
        }
    },

    getBrowserLanguage: function () {
        const language = navigator.language || 'uk';
        return language.slice(0, 2).toLowerCase();
    },

    setDocumentLanguage: function (code) {
        document.documentElement.lang = code;
    },

    setMetaDescription: function (text) {
        const meta = document.querySelector('meta[name="description"]');
        if (meta) {
            meta.setAttribute('content', text);
        }
    }
};
