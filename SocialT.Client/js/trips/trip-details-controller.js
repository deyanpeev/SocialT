(function(){
    'use strict'

    function TripDetailsController(trips, identity, $routeParams) {
        var vm = this;

        vm.identity = identity;

        var id = $routeParams.id;    //routeParams[id]

        trips.getTripById(id)
            .then(function (trip) {
                vm.trip = trip;
                console.log(trip);
            });
    }

    angular.module('myApp.controllers')
        .controller('TripDetailsController', ['trips', 'identity', '$routeParams', TripDetailsController]);
}())