(function () {
    'use strict';

    function GroupController(postsService, groups) {
        var vm = this;

        postsService.getPostsForUserGroup()
            .then(function (posts) {
                vm.posts = posts;
            });

        groups.getGroupByUser()
            .then(function (group) {
                vm.groupName = group.name;
                vm.specialtyName = group.specialtyName;
            });
    }
        
    angular.module('myApp.controllers')
        .controller('GroupController', ['postsService', 'groups', GroupController]);
}());