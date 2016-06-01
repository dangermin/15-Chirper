(function() {
    'use strict';

    angular
        .module('app')
        .factory('CommentFactory', CommentFactory);

    CommentFactory.$inject = ['$http','$q','toastr'];

    /* @ngInject */
    function CommentFactory($http,$q,toastr) {

    	var url = 'http://localhost:53101/api/comments';
    	// Get Method
        function getComments() {
        	var defer = $q.defer();

        	$http.get(url)
        	.then(function(response){
        		if(typeof response.data === 'object'){
        			defer.resolve(response);
        			toastr.success('getComments is Working');
        		} else {
        			defer.reject(response);
        			toastr.warning('getComments are not working, <br/>' + response.config.url);
        		}
        	});
        	return defer.promise;
        };

        // Post Method
        function addComment(chirpId, newComment){
        	var defer = $q.defer();
        	var data = {chirpId: chirpId, text: newComment}
        	console.log(newComment)
        	$http.post(url, data)
        		 .then(function(response){
        		 	if(typeof response.data === 'object'){
        		 		defer.resolve(response);
        		 		toastr.success('addComments are working');
        		 		getComments();
        		 	} else {
        		 		defer.reject(response);
        		 		toastr.warning('getComments are not working!<br/>' + response.config.url);
        		 	}
        		 });
        		 return	defer.promise;
        	};

    var service = {
            getComments: getComments,
            addComment: addComment
        };
        return service;
    }
})();