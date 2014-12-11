(function () {
    'use strict';

    function weatherHttpInterceptor($q) {
        return {
            responseError: function (response) {
                var message = 'Request failed.Please, refresh page or contact your system administrator.';
                    alert(message);

                return $q.reject(response);
            }
        };
    };

    weatherHttpInterceptor.$inject = ['$q'];

    angular.module('weatherApp').factory('weatherHttpInterceptor', weatherHttpInterceptor);
})();