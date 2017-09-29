(function () {
    'use strict'

    function CreateMessageController($location, $routeParams, messages, notifier) {
        var vm = this;

        var userId = $routeParams.id;

        vm.createMessage = function (newMessage) {
            newMessage.userToId = userId;
            messages.createMessage(newMessage).then(function (createdMessage) {
                notifier.success('Message sent!');
                $location.path('/messages')
            });
        }
    }

    angular.module('myApp.controllers')
        .controller('CreateMessageController', ['$location', '$routeParams', 'messages', 'notifier', CreateMessageController]);
}());