(function () {

    'use strict';

    function config($httpProvider) {
        //add interceptor for global error handling
        $httpProvider.interceptors.push('weatherHttpInterceptor');
    }

    config.$inject = ['$httpProvider'];
    angular.module('weatherApp').config(config);
})();