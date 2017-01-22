(function () {
    'use strict'

    function specialties(data) {
        function getAll() {
            return data.get('api/specialties');
        }

        return {
            getAll: getAll
        }
    }

    angular.module('myApp.services')
        .factory('specialties', ['data', specialties]);
}());