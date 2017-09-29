(function () {
    'use strict';

    function auth($http, $q, identity, authorization, baseServiceUrl) {
        var usersApi = baseServiceUrl + '/api/users'

        var signup = function (user, urlPostfix) {
            if (!urlPostfix) {
                urlPostfix = '/register';
            }

            var deferred = $q.defer();
            debugger;
            $http.post(usersApi + urlPostfix, user)
                .then(function () {
                    deferred.resolve();
                }, function (response) {
                    debugger;
                    var error = response.data.modelState;
                    if (!error) {
                        response.data.message;
                    }
                    System.log(error);
                    if (error && error[Object.keys(error)[0]][0]) {
                        error = error[Object.keys(error)[0]][0];
                    }

                    deferred.reject(error);
                });

            return deferred.promise;
        }

        return {
            signup: function (user) {
                return signup(user);
            },
            signupTeacher: function (user) {
                return signup(user, '/registerTeacher');
            },
            signupStudent: function (user) {
                return signup(user, '/registerStudent');
            },
            signupEmployer: function (user) {
                return signup(user, '/registerEmployer');
            },
            login: function (user) {
                var deferred = $q.defer();
                user['grant_type'] = 'password';
                $http.post(usersApi + '/login', 'username=' + user.username + '&password=' + user.password + '&grant_type=password', { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                    .then(function (response) {
                        if (response.data["access_token"]) {
                            identity.setCurrentUser(response.data);
                            deferred.resolve(true);
                        }
                    }, function (response) {
                        var error = response.data.error_description;
                        error = response.data.modelState;
                        if (error && error[Object.keys(error)[0]][0]) {
                            error = error[Object.keys(error)[0]][0];
                        } else {
                            error = response.data.error_description;
                        }
                        console.log(error);

                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            logout: function () {
                var deferred = $q.defer();

                var headers = authorization.getAuthorizationHeader();
                $http.post(usersApi + '/logout', {}, { headers: headers })
                    .then(function () {
                        identity.setCurrentUser(undefined);
                        deferred.resolve();
                    });

                return deferred.promise;
            },
            isAuthenticated: function () {
                if (identity.isAuthenticated()) {
                    return true;
                }
                else {
                    return $q.reject('not authorized');
                }
            }
        }
    }

    angular.module('myApp.services')
        .factory('auth', ['$http', '$q', 'identity', 'authorization', 'baseServiceUrl', auth]);
}());