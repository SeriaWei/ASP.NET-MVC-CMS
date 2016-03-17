var tieTuKu = (function () {
    function tieTuKu(accessKey, secretKey, openKey) {
        this.methd = "POST";
        this.host = "http://api.tietuku.cn/v2/api/";
        this.albumUrl = "http://api.tietuku.cn/v1/Album";
        this.picListUrl = "http://api.tietuku.cn/v1/List";
        this.picUrl = "http://api.tietuku.cn/v1/Pic";
        this.returnType = "json";
        this.onprogress = function () { };
        this.accessKey = accessKey;
        this.secretKey = secretKey;
        this.openKey = openKey;
    }
    tieTuKu.prototype.processRequest = function (formData, url, callback) {
        var xhr = new XMLHttpRequest();
        xhr.open(this.methd, url);
        xhr.onload = function (data) {
            if (data.target.status === 200) {
                callback.call(data, JSON.parse(data.target.response));
            }
            else {
                callback.call(data, JSON.parse(data.target.response));
            }
        };
        xhr.onerror = function (data) {
            console.log(data.target.response);
            callback.call(data, data.target.response);
        };
        xhr.upload.onprogress = function (e) {
            var persecnt = e.loaded / e.total * 100;
            if (onprogress) {
                onprogress.call(e, persecnt);
            }
        };
        xhr.send(formData);
    };
    tieTuKu.prototype.getToken = function (obj) {
        obj.deadline = Date.now() + 60;
        var encodedParam = CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(JSON.stringify(obj)));
        var encodedSign = CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(CryptoJS.HmacSHA1(encodedParam, this.secretKey).toString()));
        return this.accessKey + ":" + encodedSign + ":" + encodedParam;
    };
    ///全部图片列表
    tieTuKu.prototype.getNewPic = function (p, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "getnewpic", cid: 1, page_no: p }));
        this.processRequest(formData, this.picListUrl, callBack);
    };
    tieTuKu.prototype.createAlbum = function (name, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "create", albumname: name }));
        this.processRequest(formData, this.albumUrl, callBack);
    };
    tieTuKu.prototype.deleteAlbum = function (aid, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "delalbum", aid: aid }));
        this.processRequest(formData, this.albumUrl, callBack);
    };
    tieTuKu.prototype.updateAlbum = function (aid, name, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "editalbum", aid: aid, albumname: name }));
        this.processRequest(formData, this.albumUrl, callBack);
    };
    ///获取相册
    tieTuKu.prototype.getAlbum = function (p, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "get", page_no: p }));
        this.processRequest(formData, this.albumUrl, callBack);
    };
    ///获取相册内图片
    tieTuKu.prototype.getAlbumPic = function (p, aid, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "album", aid: aid, page_no: p }));
        this.processRequest(formData, this.picListUrl, callBack);
    };
    ///上传
    tieTuKu.prototype.upload = function (file, aid, callback) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ deadline: Date.now() + 60, "aid": aid }));
        formData.append("file", file.files[0]);
        this.processRequest(formData, "http://up.tietuku.cn/", callback);
    };
    tieTuKu.prototype.deletePic = function (pid, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "delpic", pid: pid }));
        this.processRequest(formData, this.picUrl, callBack);
    };
    tieTuKu.prototype.updatePic = function (pid, name, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "updatepicname", pid: pid, pname: name }));
        this.processRequest(formData, this.picUrl, callBack);
    };
    tieTuKu.prototype.getPicInfo = function (pid, callBack) {
        var formData = new FormData();
        formData.append("Token", this.getToken({ action: "getonepic", id: pid }));
        this.processRequest(formData, this.picUrl, callBack);
    };
    return tieTuKu;
})();
