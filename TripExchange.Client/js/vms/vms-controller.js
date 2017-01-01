(function () {
    'use strict';

    function VmsController(virtualMachines, identity) {
        var vm = this;
        vm.identity = identity;

        virtualMachines.getVms()
            .then(function (virtualMachines) {
                vm.virtualMachines = virtualMachines;
                console.log(virtualMachines);
            });

        vm.changeState = function(virtualMachine) {
            debugger;
            virtualMachines.changeState(virtualMachine.id, !virtualMachine.isOn);
            virtualMachine.isOn = !virtualMachine.isOn
        }
    }

    angular.module('myApp.controllers')
        .controller('VmsController', ['virtualMachines', 'identity', VmsController]);
}());