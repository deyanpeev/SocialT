(function () {
    'use strict'

    function messages(data) {
        var MESSAGES_URL = 'api/messages';

        function getAll() {
            return data.get(MESSAGES_URL);
        }

        function createMessage(message) {
            return data.post(MESSAGES_URL + '/sentmessage', message);
        }

        return {
            getAll: getAll,
            createMessage: createMessage
        }
    }

    angular.module('myApp.services')
        .factory('messages', ['data', messages]);
}());