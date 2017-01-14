(function () {
    'use strict';

    function identity($cookieStore) {
        var cookieStorageUserKey = 'currentApplicationUser';

        var currentUser;
        return {
            getCurrentUser: function () {
                var savedUser = $cookieStore.get(cookieStorageUserKey);
                if (savedUser) {
                    return savedUser;
                }

                return currentUser;
            },
            setCurrentUser: function (user) {
                if (user) {
                    $cookieStore.put(cookieStorageUserKey, user);
                }
                else {
                    $cookieStore.remove(cookieStorageUserKey);
                }

                currentUser = user;
            },
            isAuthenticated: function () {
                return !!this.getCurrentUser();
            },
            isStudent: function () {
                if (!this.isAuthenticated()) {
                    return false;
                }

                return this.getCurrentUser()['role'] == 'Student';
            },
            isAdmin: function () {
                if (!this.isAuthenticated()) {
                    return false;
                }

                return this.getCurrentUser()['role'] == 'Admin';
            },
            isEmployer: function () {
                if (!this.isAuthenticated()) {
                    return false;
                }

                return this.getCurrentUser()['role'] == 'Employer';
            },
            isTeacher: function () {
                if (!this.isAuthenticated()) {
                    return false;
                }

                return this.getCurrentUser()['role'] == 'Teacher';
            }
        }
    }

    angular.module('myApp.services').factory('identity', ['$cookieStore', identity]);
}());