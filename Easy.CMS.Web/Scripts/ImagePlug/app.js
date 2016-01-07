angular
	.module("api", ['ngRoute'])
	.config(function ($routeProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'album.html',
				controller: 'albumController'
			})
			.when('/albumpic/:aid/:name', {
				templateUrl: 'albumPic.html',
				controller: 'albumPicController'
			})
			.when('/albumpic/:aid/:name/:p', {
				templateUrl: 'albumPic.html',
				controller: 'albumPicController'
			});
	})
	.directive("ngFile", function ($parse, tuKuApi) {
		return {
			restrict: 'A',
			link: function (scope, element, attrs) {
				element.bind("change", function () {
					if (this.files.length > 0) {
						scope.$apply(function () {
							scope.loading = true;
						});
						var file = this;
						tuKuApi.getService(function (service) {
							service.upload(file, scope.aid, function (data) {
								scope.$apply(function () {
									if(!data.info){
										scope.newPic = data;
									}									
									scope.$parent.status = data;
									scope.loading = false;
								});
							});
						});

					}
				});
			}
		}
	})
	.directive("ngTrigger", function ($parse) {
		return {
			restrict: "A",
			link: function (scope, element, attrs) {
				element.bind("click", function (e) {
					angular.element(e.target).next()[0].click();
				});
			}
		}
	})
	.service("tuKuApi", function () {
		return {
			getService: function (onSuccess) {
				DbContext.getData("openKey", "1", function (setting) {
					if (!setting) {
						setting = {
							id: "1",
							AccessKey: "",
							SecretKey: "",
							OpenKey: ""
						};
						DbContext.insert("openKey",setting);
					}
					onSuccess.call(setting, new tieTuKu(setting.AccessKey, setting.SecretKey, setting.OpenKey));
				});
			}
		};
	})
	.config([
		'$compileProvider',
		function ($compileProvider) {
			$compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension):/);
		}
	])
	.controller("mainController", function ($scope) {
		$scope.status = { code: "200", info: "" };
	})
	.controller("albumController", function ($scope, tuKuApi) {
		$scope.isNeedUpdateOpenKey = false;
		$scope.showOpenKey = function () {
			$scope.isNeedUpdateOpenKey = true;
		}
		$scope.reloadAlbum = function () {
			$scope.loading = true;
			tuKuApi.getService(function (service) {
				$scope.OpenSetting = this;				
				service.getAlbum(1, function (data) {
				    $scope.$apply(function () {
				        if (!data || data === "") {
				            data = { code: "433", info: "公钥无效" };
				        }
					    $scope.albumInfo = data;
						$scope.loading = false;
						if (data.code === "433") {
							$scope.isNeedUpdateOpenKey = true;
						}
					});
				});
			});
		}
		$scope.createAlbum = function () {
			tuKuApi.getService(function (service) {
				service.createAlbum($scope.albumName, function (data) {
					$scope.reloadAlbum();
					$scope.$apply(function () {
						$scope.$parent.status = data;
					});
				});
			});
		}
		$scope.saveOpenKey = function () {
			DbContext.update("openKey",$scope.OpenSetting, function () {
				$scope.reloadAlbum();
			});
			$scope.isNeedUpdateOpenKey = false;
		}
		$scope.cancelOpenKey = function () {
			$scope.isNeedUpdateOpenKey = false;
		}
		$scope.reloadAlbum();
	})
	.controller("albumPicController", function ($scope, $routeParams, tuKuApi, $location) {
		$scope.aid = $routeParams.aid;
		$scope.albumName = $routeParams.name;
		$scope.p = parseInt($routeParams.p || 1);
		$scope.getpic = function () {
			$scope.loading = true;
			$scope.newPic = null;
			tuKuApi.getService(function (service) {
				service.getAlbumPic($scope.p, $routeParams.aid, function (data) {
					$scope.$apply(function () {
						$scope.pageArray = new Array(data.pages);
						$scope.picInfo = data;
						$scope.loading = false;
					});
				});
			});
		}
		$scope.editAlbum = function (event) {
			if (event.keyCode == 13) {
				$scope.readonly = true;
				tuKuApi.getService(function (service) {
					service.updateAlbum($scope.aid, $scope.albumName, function (data) {
						$scope.$apply(function () {
							$scope.$parent.status = data;
						});
					});
				});
			}
		}
		$scope.deleteAlbum = function () {
			if (confirm('确认要删除相册吗？')) {
				tuKuApi.getService(function (service) {
					service.deleteAlbum($scope.aid, function (data) {
						$scope.$apply(function () {
							$scope.$parent.status = data;
							if (data.code == '200') {
								$location.path('/');
							}
						});

					});
				});
			}
		}
		$scope.deletePic = function (pid) {
			if (confirm('确认要删除图片吗？')) {
				tuKuApi.getService(function (service) {
					service.deletePic(pid, function (data) {
						$scope.$apply(function () {
							$scope.$parent.status = data;
						});
						$scope.getpic();
					});
				});
			}
		}
		$scope.getpic();
	});