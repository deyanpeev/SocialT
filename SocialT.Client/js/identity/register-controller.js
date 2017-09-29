(function () {
    'use strict';

    var STUDENT = 'Student';
    var EMPLOYER = 'Employer';

    function RegisterController($scope, $location, auth, notifier, specialties, groups) {
        $scope.markStudent = function () {
            $scope.userType = STUDENT;
        }

        $scope.markEmployer = function () {
            $scope.userType = EMPLOYER;
        }

        $scope.signup = function (user, isTeacher) {
            var promise = null;
            if ($scope.userType == STUDENT) {
                promise = auth.signupStudent(user);
            } else if ($scope.userType == EMPLOYER) {
                promise = auth.signupEmployer(user);
            } else if (isTeacher) {
                promise = auth.signupTeacher(user);
            }
            
            promise.then(function () {
                notifier.success('Registration successful!');
                $location.path('/');
            }, function (error) {
                notifier.error(error);
            })
        }

        specialties.getAll()
            .then(function (allSpecialties) {
                $scope.specialties = allSpecialties;
            });

        groups.getAll()
            .then(function (allGroups) {
                $scope.groups = allGroups;
            });
    }

    angular.module('myApp.controllers')
        .controller('SignUpCtrl', ['$scope', '$location', 'auth', 'notifier', 'specialties', 'groups', RegisterController]);
}());