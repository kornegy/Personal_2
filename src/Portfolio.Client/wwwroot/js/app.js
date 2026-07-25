// Небольшие браузерные операции, недоступные из C#: хранилище, язык браузера,
// атрибуты документа, появление блоков и трансформации при прокрутке.
// Отдельный файл вместо инлайн-скрипта — так Content-Security-Policy остаётся строгой.
window.portfolioApp = {
    storageKey: 'portfolio-language',
    revealObserver: null,
    scrollBound: false,

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

    prefersReducedMotion: function () {
        return window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    },

    // Вызывается из Blazor после отрисовки: до этого нужных элементов в DOM ещё нет.
    // Повторный вызов безопасен — уже показанные блоки пропускаются.
    initEffects: function () {
        window.portfolioApp.initReveal();
        window.portfolioApp.initScrollDepth();
    },

    // Блоки с классом reveal проявляются, когда доходят до экрана.
    initReveal: function () {
        const app = window.portfolioApp;
        const elements = document.querySelectorAll('.reveal:not(.is-visible), .rule:not(.is-visible)');

        if (app.prefersReducedMotion()) {
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
    },

    // Задник первого экрана наезжает, имя одновременно отдаляется —
    // это даёт ощущение глубины, как при смене фокуса объектива.
    initScrollDepth: function () {
        const app = window.portfolioApp;

        if (app.scrollBound || app.prefersReducedMotion()) {
            return;
        }

        const apply = function () {
            const background = document.querySelector('[data-noir-bg]');
            const title = document.querySelector('[data-noir-title]');
            if (!background && !title) {
                return;
            }

            // Весь эффект укладывается в первые 30% высоты экрана.
            const distance = window.innerHeight * 0.3;
            const progress = Math.min(Math.max(window.scrollY / distance, 0), 1);

            if (background) {
                background.style.transform = 'scale(' + (1 + 0.27 * progress).toFixed(4) + ')';
            }

            if (title) {
                title.style.transform = 'scale(' + (1 - 0.11 * progress).toFixed(4) + ')';
                title.style.opacity = (1 - 0.35 * progress).toFixed(3);
            }
        };

        let ticking = false;
        window.addEventListener('scroll', function () {
            if (!ticking) {
                ticking = true;
                window.requestAnimationFrame(function () {
                    apply();
                    ticking = false;
                });
            }
        }, { passive: true });

        window.addEventListener('resize', apply, { passive: true });
        app.scrollBound = true;
        apply();
    }
};
