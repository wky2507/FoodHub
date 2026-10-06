window.imageViewer = {

    init: function (id) {

        const element = document.getElementById(id);

        if (!element) {
            return;
        }

        if (element._wheelHandler) {
            return;
        }

        element._wheelHandler = function (e) {

            // ��ֹ����ҳ����Ź���
            e.preventDefault();

        };

        element.addEventListener(
            "wheel",
            element._wheelHandler,
            {
                passive: false
            }
        );
    },

    destroy: function (id) {

        const element = document.getElementById(id);

        if (!element) {
            return;
        }

        if (element._wheelHandler) {

            element.removeEventListener(
                "wheel",
                element._wheelHandler
            );

            element._wheelHandler = null;
        }
    }
};