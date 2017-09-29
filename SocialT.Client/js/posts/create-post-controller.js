(function () {
    'use strict';

    function CreatePostController($location, identity, notifier, postsService, specialties) {
        var vm = this;
        vm.identity = identity;

        vm.createSpecialtyPost = function (post) {
            postsService
        }

        debugger;
        vm.createGroupPost = function (post) {
            postsService.createGroupPost(post).then(function (post) {
                notifier.success('Post successfully created!');
                $location.path('/group');
            });
        }

        vm.createSpecialtyPost = function (post) {
            postsService.createSpecialtyPost(post).then(function (post) {
                notifier.success('Post successfully created!');
                debugger;
                if (vm.identity.isStudent()) {
                    $location.path('/specialty');
                } else {
                    $location.path('/');
                }
            });
        }

        specialties.getAll()
            .then(function (allSpecialties) {
                vm.specialties = allSpecialties;
            });
    }
        
    angular.module('myApp.controllers')
        .controller('CreatePostController', ['$location', 'identity', 'notifier', 'postsService', 'specialties', CreatePostController]);
}());