var driveApp = angular.module('driveApp', [])
    .controller("DriveCtrl", function ($scope, $http) {
        $scope.currentDirectory = {
            currentPath: '',
            folders: [],
            files: [],
            less10mb: 0,
            between10_50mb: 0,
            more100mb: 0,

            // Get folders and files in directory.
            getItems: function (folder) {
                var path = (this.currentPath == '') ? folder : this.currentPath + "\\" + folder;
                return $http.get('/api/drive/directory', { params: { path: path } })
                    .success(function (data) {
                        $scope.currentDirectory.currentPath = data.CurrentPath;
                        $scope.currentDirectory.folders = data.Folders;
                        $scope.currentDirectory.files = data.Files;

                        // Start calculate count files for current directory.
                        this.currentDirectory.getCountFiles(this.currentPath);
                    });
            },

            // Get count files in directory and subdirectories. 
            getCountFiles: function (path) {
                return $http.get('api/drive/countFiles', { params: { path: path } })
                    .success(function (data) {
                        $scope.currentDirectory.less10mb = data.CountFilesLessThat_10mb;
                        $scope.currentDirectory.between10_50mb = data.CountFiles_10_50mb;
                        $scope.currentDirectory.more100mb = data.CountFilesMoreThat_100mb;
                    });
            }
        };

        $scope.initCurrentDirectory = function() {
            // Init data in currentDirectory object for first use.
            $scope.currentDirectory.getItems();
        }();
        
    });


