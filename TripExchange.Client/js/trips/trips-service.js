(function(){
    'use strict'

    function trips(data) {
        var TRIPS_URL = 'api/trips';

        function getPublicTrips() {
            return data.get(TRIPS_URL);
        }

        function createTrip(trip){
            return data.post(TRIPS_URL, trip);
        }

        function getTripById(tripId) {
            return data.get(TRIPS_URL + '/' + tripId);
        }

        function filterTrips(filters) {
            return data.get(TRIPS_URL, filters);
        }

        return {
            getPublicTrips: getPublicTrips,
            createTrip: createTrip,
            getTripById: getTripById,
            filterTrips: filterTrips
        }
    }

    angular.module('myApp.services')
        .factory('trips', ['data', trips])
}());