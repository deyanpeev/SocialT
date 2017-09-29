(function () {
    'use strict'

    function ShowProfileController($route, data, identity, $routeParams) {
        var vm = this;
        var USERS_URL = 'api/users';

        vm.identity = identity;

        var id = $routeParams.id;

        var promise = null;
        if (id == null) {
            promise = data.get(USERS_URL + '/GetUserById');
        } else {
            promise = data.get(USERS_URL + '/GetUserById?id=' + id);
        }
        promise.then(function (user) {
                vm.profile = user;
                vm.isStudent = (user.roleName == 'Student');
                vm.isEmployer = (user.roleName == 'Employer');
                console.log('DP roleName - ' + user.roleName);
        });

        vm.addSkill = function (skillName) {
            data.post('api/skills/AddSkill', JSON.stringify(skillName))
                .then(function () {
                    $route.reload();
                });
        }
    }

    angular.module('myApp.controllers')
        .controller('ShowProfileController', ['$route', 'data', 'identity', '$routeParams', ShowProfileController]);
}())