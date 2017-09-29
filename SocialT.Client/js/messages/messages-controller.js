(function () {
    'use strict';

    function MessagesController(messages, identity) {
        var vm = this;
        vm.currentUserName = identity.getCurrentUser();

        messages.getAll()
            .then(function (messages) {
                debugger;
                vm.messages = messages;
            });
    }

    angular.module('myApp.controllers')
        .controller('MessagesController', ['messages', 'identity', MessagesController]);
}());