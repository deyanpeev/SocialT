(function(){
    'use strict'

    function allVms() {
        return {
            restrict: 'A',
            templateUrl: 'views/directives/all-vms-directive.html'
        }
    }

    angular.module('myApp.directives')
        .directive('allVms', [allVms]);
}());