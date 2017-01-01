(function () {
    'use strict'

    function CreateVmController($location, virtualMachines) {
        var vm = this;

        vm.createVm = function (newVm) {
            debugger;
            virtualMachines.createVm(newVm).then(function (createdVm) {
                $location.path('/virtualMachines')
            });
        }
    }

    angular.module('myApp.controllers')
        .controller('CreateVmController', ['$location', 'virtualMachines', CreateVmController]);
}());