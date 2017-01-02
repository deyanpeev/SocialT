(function(){
    'use strict'

    function beautifulDate(){
        return function(input){
            return 'kor';
        }
    }

    angular.module('myApp.filters')
        .filter('beautifulDate', [beautifulDate])
}());