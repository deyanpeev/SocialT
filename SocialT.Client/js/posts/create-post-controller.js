(function () {
    'use strict';

    function CreatePostController($location, identity, postsService, specialties) {
        var vm = this;
        vm.identity = identity;

        vm.createSpecialtyPost = function (post) {
            postsService
        }

        vm.createGroupPost = function (post) {
            debugger;
            postsService.createGroupPost(post).then(function (post) {
                debugger;
                $location.path('/group');
            });
        }

        vm.createSpecialtyPost = function (post) {
            debugger;
            postsService.createSpecialtyPost(post).then(function (post) {
                debugger;
                $location.path('/specialty');
            });
        }

        specialties.getAll()
            .then(function (allSpecialties) {
                vm.specialties = allSpecialties;
            });
    }
        
    angular.module('myApp.controllers')
        .controller('CreatePostController', ['$location', 'identity', 'postsService', 'specialties', CreatePostController]);
}());