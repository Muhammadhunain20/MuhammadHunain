/* ================================================================
   MUHAMMAD HUNAIN – PORTFOLIO JS
================================================================ */

document.addEventListener('DOMContentLoaded', () => {

  // ────────────────────────────────────────────────────────────
  // 1. AOS – ANIMATE ON SCROLL
  // ────────────────────────────────────────────────────────────
  AOS.init({
    duration: 800,
    easing: 'ease-out-quart',
    once: true,
    offset: 60,
  });

  // ────────────────────────────────────────────────────────────
  // 2. TYPED.JS – ROLE ANIMATION
  // ────────────────────────────────────────────────────────────
  if (document.getElementById('typed-role')) {
    new Typed('#typed-role', {
      strings: [
        'Web Developer',
        'Frontend Engineer',
        'ASP.NET Developer',
        'UI Enthusiast',
        'CS Final Year Student',
      ],
      typeSpeed: 65,
      backSpeed: 35,
      backDelay: 1800,
      startDelay: 400,
      loop: true,
      cursorChar: '|',
    });
  }

  // ────────────────────────────────────────────────────────────
  // 3. CANVAS PARTICLE NETWORK
  // ────────────────────────────────────────────────────────────
  const canvas = document.getElementById('particles-canvas');
  if (canvas) {
    const ctx = canvas.getContext('2d');
    let particles = [];
    const COUNT = window.innerWidth < 768 ? 50 : 100;
    const CONNECT_DIST = 130;
    let mouse = { x: null, y: null };

    function resize() {
      canvas.width  = window.innerWidth;
      canvas.height = window.innerHeight;
    }
    resize();
    window.addEventListener('resize', () => { resize(); initParticles(); });

    document.addEventListener('mousemove', e => {
      mouse.x = e.clientX;
      mouse.y = e.clientY;
    });

    class Particle {
      constructor() { this.reset(); }
      reset() {
        this.x  = Math.random() * canvas.width;
        this.y  = Math.random() * canvas.height;
        this.vx = (Math.random() - 0.5) * 0.45;
        this.vy = (Math.random() - 0.5) * 0.45;
        this.size = Math.random() * 1.8 + 0.5;
        this.opacity = Math.random() * 0.45 + 0.1;
        this.hue = Math.random() > 0.55 ? 'violet' : 'teal';
      }
      update() {
        this.x += this.vx;
        this.y += this.vy;
        if (this.x < 0 || this.x > canvas.width)  this.vx *= -1;
        if (this.y < 0 || this.y > canvas.height)  this.vy *= -1;
        // slight mouse repulsion
        if (mouse.x !== null) {
          const dx = this.x - mouse.x;
          const dy = this.y - mouse.y;
          const dist = Math.sqrt(dx * dx + dy * dy);
          if (dist < 80) {
            this.x += dx * 0.015;
            this.y += dy * 0.015;
          }
        }
      }
      draw() {
        const color = this.hue === 'violet' ? '124,111,255' : '0,229,212';
        ctx.beginPath();
        ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2);
        ctx.fillStyle = `rgba(${color},${this.opacity})`;
        ctx.fill();
      }
    }

    function initParticles() {
      particles = Array.from({ length: COUNT }, () => new Particle());
    }

    function drawConnections() {
      for (let i = 0; i < particles.length; i++) {
        for (let j = i + 1; j < particles.length; j++) {
          const dx = particles[i].x - particles[j].x;
          const dy = particles[i].y - particles[j].y;
          const d  = Math.sqrt(dx * dx + dy * dy);
          if (d < CONNECT_DIST) {
            const alpha = (1 - d / CONNECT_DIST) * 0.12;
            ctx.beginPath();
            ctx.moveTo(particles[i].x, particles[i].y);
            ctx.lineTo(particles[j].x, particles[j].y);
            ctx.strokeStyle = `rgba(124,111,255,${alpha})`;
            ctx.lineWidth   = 0.6;
            ctx.stroke();
          }
        }
      }
    }

    function animate() {
      ctx.clearRect(0, 0, canvas.width, canvas.height);
      particles.forEach(p => { p.update(); p.draw(); });
      drawConnections();
      requestAnimationFrame(animate);
    }

    initParticles();
    animate();
  }

  // ────────────────────────────────────────────────────────────
  // 4. NAVBAR – SCROLL EFFECT & ACTIVE LINK
  // ────────────────────────────────────────────────────────────
  const navbar = document.getElementById('mainNav');
  const navLinks = document.querySelectorAll('.nav-link-custom');
  const sections = document.querySelectorAll('section[id]');

  function updateNav() {
    // Scroll effect
    if (navbar) {
      navbar.classList.toggle('scrolled', window.scrollY > 50);
    }

    // Active link highlight
    let current = '';
    sections.forEach(sec => {
      if (window.scrollY >= sec.offsetTop - 120) current = sec.id;
    });

    navLinks.forEach(link => {
      link.classList.remove('active');
      if (link.getAttribute('href') === '#' + current) {
        link.classList.add('active');
      }
    });
  }

  window.addEventListener('scroll', updateNav, { passive: true });
  updateNav();

  // ────────────────────────────────────────────────────────────
  // 5. SMOOTH SCROLL (Bootstrap handles most, this adds extra)
  // ────────────────────────────────────────────────────────────
  document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', e => {
      const target = document.querySelector(anchor.getAttribute('href'));
      if (target) {
        e.preventDefault();
        // Close mobile nav if open
        const bsCollapse = document.querySelector('.navbar-collapse');
        if (bsCollapse && bsCollapse.classList.contains('show')) {
          bsCollapse.classList.remove('show');
        }
        target.scrollIntoView({ behavior: 'smooth' });
      }
    });
  });

  // ────────────────────────────────────────────────────────────
  // 6. ANIMATED SKILL BARS (trigger on scroll into view)
  // ────────────────────────────────────────────────────────────
  const skillBars = document.querySelectorAll('.skill-bar-fill');

  const skillObserver = new IntersectionObserver(entries => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        const bar = entry.target;
        const targetWidth = bar.getAttribute('data-width');
        setTimeout(() => { bar.style.width = targetWidth + '%'; }, 200);
        skillObserver.unobserve(bar);
      }
    });
  }, { threshold: 0.3 });

  skillBars.forEach(bar => skillObserver.observe(bar));

  // ────────────────────────────────────────────────────────────
  // 7. CONTACT FORM – AJAX SUBMISSION
  // ────────────────────────────────────────────────────────────
  const form       = document.getElementById('contactForm');
  const submitBtn  = document.getElementById('submitBtn');
  const btnLoader  = document.getElementById('btnLoader');
  const successBox = document.getElementById('form-success');
  const errorBox   = document.getElementById('form-error');

  if (form) {
    form.addEventListener('submit', async e => {
      e.preventDefault();

      // UI: loading state
      submitBtn.disabled = true;
      if (btnLoader) btnLoader.classList.remove('d-none');
      submitBtn.querySelector('i.fa-paper-plane')?.classList.add('d-none');
      if (successBox) successBox.classList.add('d-none');
      if (errorBox)   errorBox.classList.add('d-none');

      const token = document.querySelector('input[name="__RequestVerificationToken"]');

      try {
        const formData = new URLSearchParams({
          name:    form.querySelector('[name="name"]').value.trim(),
          email:   form.querySelector('[name="email"]').value.trim(),
          subject: form.querySelector('[name="subject"]').value.trim(),
          message: form.querySelector('[name="message"]').value.trim(),
          __RequestVerificationToken: token ? token.value : '',
        });

        const res = await fetch('/Home/SubmitContact', {
          method:  'POST',
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          body:    formData.toString(),
        });

        const data = await res.json();

        if (data.success) {
          if (successBox) {
            document.getElementById('form-success-text').textContent = data.message;
            successBox.classList.remove('d-none');
          }
          form.reset();
        } else {
          if (errorBox) {
            document.getElementById('form-error-text').textContent = data.message;
            errorBox.classList.remove('d-none');
          }
        }
      } catch {
        if (errorBox) {
          document.getElementById('form-error-text').textContent =
            'Something went wrong. Please email me directly.';
          errorBox.classList.remove('d-none');
        }
      } finally {
        submitBtn.disabled = false;
        if (btnLoader) btnLoader.classList.add('d-none');
        submitBtn.querySelector('i.fa-paper-plane')?.classList.remove('d-none');
      }
    });
  }

  // ────────────────────────────────────────────────────────────
  // 8. VANILLA TILT – 3D CARD EFFECT
  // ────────────────────────────────────────────────────────────
  if (typeof VanillaTilt !== 'undefined') {
    VanillaTilt.init(document.querySelectorAll('[data-tilt]'), {
      max:          6,
      speed:        400,
      glare:        true,
      'max-glare':  0.08,
      perspective:  1200,
    });
  }

  // ────────────────────────────────────────────────────────────
  // 9. PAGE ENTRANCE – FADE IN BODY
  // ────────────────────────────────────────────────────────────
  document.body.style.opacity = '0';
  document.body.style.transition = 'opacity 0.5s ease';
  requestAnimationFrame(() => {
    requestAnimationFrame(() => { document.body.style.opacity = '1'; });
  });

});
