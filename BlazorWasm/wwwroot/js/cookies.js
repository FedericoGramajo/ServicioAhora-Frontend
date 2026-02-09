window.getCookie = (key) => {
    const match = document.cookie.match('(^|;) ?' + encodeURIComponent(key) + '=([^;]*)(;|$)');
    return match ? decodeURIComponent(match[2]) : null;
};
