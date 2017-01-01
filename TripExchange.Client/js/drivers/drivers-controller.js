(function () {
    'use strict';

    function DriversController(drivers) {
        var vm = this;

        drivers.getPublicDrivers()
            .then(function (publicDrivers) {
                vm.drivers = publicDrivers;
            });
    }

    angular.module('myApp.controllers')
        .controller('DriversController', ['drivers', DriversController]);
}());