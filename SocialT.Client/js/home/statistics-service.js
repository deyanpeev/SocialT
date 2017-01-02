(function(){
    'use strict';

    function statistics($q, data) {

        var statistics;

        function getStats() {
            //caching
            //cache could be done in the data in some sort of a dictionary
            if (statistics) {
                return $q.when(statistics); //when resolves immediately, returns the promise right away
            } else {
                return data.get('api/stats')
                    .then(function (stats) {
                        statistics = stats; 
                        return stats;
                    });
            }
        }

        return {
            getStats: getStats
        }
    }

    angular.module('myApp.services')
        .factory('statistics', ['$q', 'data', statistics]);
}());