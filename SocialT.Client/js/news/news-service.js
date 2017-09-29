(function () {
    'use strict'

    function news(data) {
        var NEWS_URL = 'api/news';

        function getAll() {
            return data.get(NEWS_URL);
        }

        function createNews(news) {
            return data.post(NEWS_URL + '/createNews', news);
        }

        return {
            getAll: getAll,
            createNews: createNews
        }
    }

    angular.module('myApp.services')
        .factory('news', ['data', news]);
}());