(function() {
    'use strict';

    angular
        .module('app')
        .factory('PostsFactory', PostsFactory);

    PostsFactory.$inject = ['$http', '$q', 'toastr'];

    /* @ngInject */
    function PostsFactory($http, $q, toastr) {

        var url = 'http://localhost:53101/api/chirps';
        // Get Method
        function getPosts() {
        	var defer = $q.defer();

        	$http.get(url)
        		.then(function(response){
        			if(typeof response.data === 'object') {
        				defer.resolve(response);
        				toastr.success('getPosts is Working!');
        			} else {
        				defer.reject(response);
        				toastr.warning('getPosts get is not working!<br/>' + response.config.url);
        			}
        		});
        		return defer.promise;
        };
        // Post Method
        function addPost(newPost){
        	var defer = $q.defer();
        	
        	$http.post(url, newPost)
        		 .then(function(response){
        		 	if(typeof response.data === 'object') {
        		 		defer.resolve(response);
        		 		toastr.success('addPost is Working!');
        		 	} else {
        		 		defer.reject(response);
        		 		toastr.warning('addPost is not working!<br/> ' + response.config.url);
        		 	}
        		 });
        		 return defer.promise;
        };
        // Put Method
        function likePost(post){
        	var defer =$q.defer();

        	$http.put(url + '/' + post.chirpId)
        		 .then(function(response){
        		 	if(typeof response.data === 'object'){
        		 		defer.resolve(response);
        		 		toastr.success('Like is working!');
        		 	} else {
        		 		defer.reject(response);
        		 		toastr.warning('Like is not working <br/>' + response.config.url);
        		 	}
        		 });
        		 return defer.promise;
        };
        // Delete Method
        function deletePost(post){
        	var defer = $q.defer();

        	$http.delete(url + '/' + post.chirpId)
        		 .then(function(response){
        		 	defer.resolve(response)
        		 })
        	return defer.promise;
        };

        var service = {
            getPosts: getPosts,
            addPost: addPost,
            likePost: likePost,
            deletePost: deletePost
        };
        return service;
    }
})();