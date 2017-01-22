(function () {
    'use strict';

    function SpecialtyController(postsService) {
        var vm = this;

        postsService.getAllPostsForUserSpecialty()
            .then(function (posts) {
                debugger;
                vm.posts = posts;
            });
    }
        
    angular.module('myApp.controllers')
        .controller('SpecialtyController', ['postsService', SpecialtyController]);
}());