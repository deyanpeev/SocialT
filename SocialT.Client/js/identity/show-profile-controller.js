(function () {
    'use strict'

    function ShowProfileController(data, identity, $routeParams) {
        var vm = this;
        var USERS_URL = 'api/users';

        vm.identity = identity;

        var id = $routeParams.id;
        debugger;
        var promise = null;
        if (id == null) {
            promise = data.get(USERS_URL + '/GetUserById');
        } else {
            promise = data.get(USERS_URL + '/GetUserById?id=' + id);
        }
        promise.then(function (user) {
                debugger;
                vm.profile = user;
                vm.isStudent = (user.roleName == 'Student');
                vm.isEmployer = (user.roleName == 'Employer');
                console.log('DP roleName - ' + user.roleName);
            });
    }

    angular.module('myApp.controllers')
        .controller('ShowProfileController', ['data', 'identity', '$routeParams', ShowProfileController]);
}())