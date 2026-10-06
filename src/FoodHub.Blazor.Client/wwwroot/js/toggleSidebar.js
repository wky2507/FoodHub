window.toggleSidebar = function () {
    // SB Admin 切换侧边栏的原理就是在 <body> 上切换 sb-sidenav-toggled 类
    document.body.classList.toggle('sb-sidenav-toggled');

    // 如果模板将状态存在了本地存储，可以顺便同步（可选）
    localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
};