(function(){
    'use strict'

    function postsService(data) {
        var VM_URL = 'api/posts';

        function getPostsForUserGroup() {
            return data.get(VM_URL + '/GetAllPostsForUserGroup');
        }

        function getAllPostsForUserSpecialty() {
            return data.get(VM_URL + '/GetAllPostsForUserSpecialty');
        }

        function createSpecialtyPost(post) {
            return data.post(VM_URL + '/CreateNewSpecialtyPost', post);
        }

        function createGroupPost(post) {
            debugger;
            return data.post(VM_URL + '/CreateNewGroupPost', post);
        }

        return {
            getPostsForUserGroup: getPostsForUserGroup,
            getAllPostsForUserSpecialty: getAllPostsForUserSpecialty,
            createSpecialtyPost: createSpecialtyPost,
            createGroupPost: createGroupPost
        }
    }

    angular.module('myApp.services')
        .factory('postsService', ['data', postsService])
}());