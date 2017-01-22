(function(){
    'use strict'

    function usersService(data) {
        var VM_URL = 'api/users';

        function getUnactivatedUsers() {
            return data.get(VM_URL + '/GetUserByActivation');
        }

        function changeActiveState(userId, isActivated) {
            var url = VM_URL + '/ChangeUserActiveState?userId=' + userId + '&active=' + isActivated
            return data.post(url);
        }

        return {
            getUnactivatedUsers: getUnactivatedUsers,
            changeActiveState: changeActiveState
        }
    }

    angular.module('myApp.services')
        .factory('usersService', ['data', usersService])
}());