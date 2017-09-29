(function () {
    'use strict';

    function SpecialtyController(postsService, groups) {
        var vm = this;

        postsService.getAllPostsForUserSpecialty()
            .then(function (posts) {
                debugger;
                vm.posts = posts;
            });

        groups.getGroupByUser()
            .then(function (group) {
                vm.specialtyName = group.specialtyName;
            });
    }
        
    angular.module('myApp.controllers')
        .controller('SpecialtyController', ['postsService', 'groups', SpecialtyController]);
}());