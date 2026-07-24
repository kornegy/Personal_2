// Small vanilla-JS helpers used by the Blazor client through JS interop.
window.site = {
    getTheme: function () {
        return document.documentElement.getAttribute('data-theme') || 'dark';
    },
    setTheme: function (theme) {
        document.documentElement.setAttribute('data-theme', theme);
        try { localStorage.setItem('theme', theme); } catch (e) { }
        return theme;
    },
    toggleTheme: function () {
        var next = this.getTheme() === 'dark' ? 'light' : 'dark';
        return this.setTheme(next);
    },
    scrollToId: function (id) {
        var el = document.getElementById(id);
        if (el) el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    },
    // Open the visitor's email client with a pre-filled message.
    openMail: function (to, subject, body) {
        var url = 'mailto:' + encodeURIComponent(to) +
            '?subject=' + encodeURIComponent(subject) +
            '&body=' + encodeURIComponent(body);
        window.location.href = url;
    }
};

// Reveal-on-scroll + navbar background + active-section tracking are wired up
// after Blazor renders the page. A MutationObserver waits for the content.
window.site.initScrollFx = function () {
    // Fade sections in as they enter the viewport.
    var reveals = document.querySelectorAll('.reveal');
    if ('IntersectionObserver' in window) {
        var io = new IntersectionObserver(function (entries) {
            entries.forEach(function (e) {
                if (e.isIntersecting) {
                    e.target.classList.add('is-visible');
                    io.unobserve(e.target);
                }
            });
        }, { threshold: 0.12 });
        reveals.forEach(function (r) { io.observe(r); });
    } else {
        reveals.forEach(function (r) { r.classList.add('is-visible'); });
    }

    // Solidify the navbar once the user scrolls past the hero.
    var nav = document.querySelector('.navbar-shell');
    var onScroll = function () {
        if (!nav) return;
        if (window.scrollY > 40) nav.classList.add('is-scrolled');
        else nav.classList.remove('is-scrolled');
    };
    window.addEventListener('scroll', onScroll, { passive: true });
    onScroll();

    // Highlight the nav link for the section currently in view.
    var sections = document.querySelectorAll('section[id]');
    if ('IntersectionObserver' in window) {
        var spy = new IntersectionObserver(function (entries) {
            entries.forEach(function (e) {
                if (e.isIntersecting) {
                    document.querySelectorAll('.nav-link-dot').forEach(function (l) {
                        l.classList.toggle('active', l.getAttribute('data-target') === e.target.id);
                    });
                }
            });
        }, { rootMargin: '-45% 0px -50% 0px' });
        sections.forEach(function (s) { spy.observe(s); });
    }
};
