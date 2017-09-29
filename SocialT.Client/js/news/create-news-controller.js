(function () {
    'use strict'

    function CreateNewsController($location, news, notifier) {
        var vm = this;

        vm.createNews = function (newNews) {
            news.createNews(newNews).then(function (response) {
                notifier.success('News successfully created!');
                $location.path('/')
            });
        }
    }

    angular.module('myApp.controllers')
        .controller('CreateNewsController', ['$location', 'news', 'notifier', CreateNewsController]);
}());