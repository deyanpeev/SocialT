(function () {
    'use strict'

    function groups(data) {
        function getAll() {
            return data.get('api/groups');
        }

        function getGroupByUser() {
            return data.get('api/groups/GetGroupByCurrentUser');
        }

        return {
            getAll: getAll,
            getGroupByUser: getGroupByUser
        }
    }

    angular.module('myApp.services')
        .factory('groups', ['data', groups]);
}());