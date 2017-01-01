(function(){
    'use strict'

    function virtualMachines(data) {
        var VM_URL = 'api/VirtualMachines';

        function getVms() {
            return data.get(VM_URL);
        }

        function changeState(vmId, isOn) {
            var url = VM_URL + '/ChangeActiveState?id={' + vmId + '}&isOn=' + isOn
            return data.post(url);
        }

        function createVm(newVm) {
            var url = VM_URL + '/AddVirtualMachine'
            return data.post(url, newVm);
        }

        return {
            getVms: getVms,
            changeState: changeState,
            createVm: createVm
        }
    }

    angular.module('myApp.services')
        .factory('virtualMachines', ['data', virtualMachines])
}());