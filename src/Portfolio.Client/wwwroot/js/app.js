// Небольшие браузерные операции, недоступные из C#: хранилище, язык браузера,
// атрибуты документа и появление блоков при прокрутке.
// Отдельный файл вместо инлайн-скрипта — так Content-Security-Policy остаётся строгой.
window.portfolioApp = {
    storageKey: 'portfolio-language',
    revealObserver: null,

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
    },

    // Блоки с классом reveal проявляются, когда доходят до экрана.
    // Вызывается из Blazor после отрисовки: до этого нужных элементов в DOM ещё нет.
    initReveal: function () {
        const app = window.portfolioApp;
        const elements = document.querySelectorAll('.reveal:not(.is-visible)');

        // Если система просит меньше движения — просто показываем всё сразу.
        if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
            elements.forEach(el => el.classList.add('is-visible'));
            return;
        }

        if (!app.revealObserver) {
            app.revealObserver = new IntersectionObserver(function (entries, observer) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('is-visible');
                        observer.unobserve(entry.target);
                    }
                });
            }, { rootMargin: '0px 0px -10% 0px', threshold: 0.05 });
        }

        elements.forEach(el => app.revealObserver.observe(el));
    }
};
