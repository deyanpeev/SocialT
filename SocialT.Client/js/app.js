(function () {
    'use strict';

    function config($routeProvider) {

        var PARTIALS_PREFIX = 'views/partials/';
        var CONTROLLER_AS_VIEW_MODEL = 'vm';

        $routeProvider
            .when('/', {
                templateUrl: PARTIALS_PREFIX + 'home/home.html',
                controller: 'HomeController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/unauthorized', {
                templateUrl: PARTIALS_PREFIX + 'home/unauthorized.html'
            })
            .when('/trips',{
                templateUrl: PARTIALS_PREFIX + 'trips/all-trips.html',
                controller: 'TripsController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/trips/create', {
                templateUrl: PARTIALS_PREFIX + 'trips/create-trip.html',
                controller: 'CreateTripController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/trip/details/:id',{
                templateUrl: PARTIALS_PREFIX + 'trips/trip-details.html',
                controller: 'TripDetailsController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/drivers', {
                templateUrl:PARTIALS_PREFIX + 'drivers/all-drivers.html',
                controller: 'DriversController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/register', {
                templateUrl: PARTIALS_PREFIX + 'identity/register.html',
                controller: 'SignUpCtrl'
            })
            .when('/virtualMachines', {
                templateUrl: PARTIALS_PREFIX + 'vms/all-vms.html',
                controller: 'VmsController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .when('/virtualMachines/create', {
                templateUrl: PARTIALS_PREFIX + 'vms/create-vm.html',
                controller: 'CreateVmController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .otherwise({ redirectTo: '/' });
    }

    angular.module('myApp.services', []);
    angular.module('myApp.directives', []);
    angular.module('myApp.filters', []);
    angular.module('myApp.controllers', ['myApp.services']);
    angular.module('myApp', ['ngRoute', 'ngCookies', 'myApp.controllers', 'myApp.directives', 'kendo.directives', 'myApp.filters']).
        config(['$routeProvider', config])
        .value('toastr', toastr)
        .constant('baseServiceUrl', 'http://localhost:1337');
}());