(function () {
    'use strict';

    function ManageUsersController(usersService, identity) {
        var vm = this;
        vm.identity = identity;

        usersService.getUnactivatedUsers()
            .then(function (users) {
                vm.users = users;
            });

        vm.changeActiveState = function (user) {
            debugger;
            usersService.changeActiveState(user.userId, !user.isActive);
            user.isActive = !user.isActive
        }
    }

    angular.module('myApp.controllers')
        .controller('ManageUsersController', ['usersService', 'identity', ManageUsersController]);
}());