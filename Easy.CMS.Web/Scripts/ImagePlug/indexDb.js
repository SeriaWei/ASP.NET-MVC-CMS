var DbContext = {
	name: "tuku",
	DataBase: null,
	wait: 500,
	virsion: 1,
	tables: [{ name: "openKey", schema: { keyPath: "id", autoIncrement: false } }],
	getData: function (tableName, key, onSuccess) {
		if (!DbContext.DataBase) {
			setTimeout(function () {
				DbContext.getData(tableName, key, onSuccess);
			}, DbContext.wait += 500);
			return;
		}		
		DbContext.DataBase
			.transaction([tableName], "readonly")
			.objectStore(tableName)
			.get(key).onsuccess = function (e) {
				if (onSuccess) {
					onSuccess.call(DbContext, e.target.result);
				}
			}
	},
	insert: function (tableName, obj, onSuccess) {
		DbContext.DataBase
			.transaction([tableName], "readwrite")
			.objectStore(tableName)
			.add(obj).onsuccess = function (e) {
				if (onSuccess) {
					onSuccess.call(DbContext, e.target.result);
				}
			}
	},
	update: function (tableName, obj, onSuccess) {
		DbContext.DataBase
			.transaction([tableName], "readwrite")
			.objectStore(tableName)
			.put(obj).onsuccess = function (e) {
				if (onSuccess) {
					onSuccess.call(DbContext, e.target.result);
				}
			};
	},
	deleteData:function(tableName,key,onSuccess){
		DbContext.DataBase
			.transaction([tableName], "readwrite")
			.objectStore(tableName)
			.delete(key).onsuccess = function (e) {
				if (onSuccess) {
					onSuccess.call(DbContext, e.target.result);
				}
			};
	}
}

var dbRequest = window.indexedDB.open(DbContext.name, DbContext.virsion);
dbRequest.onsuccess = function (e) {
	DbContext.DataBase = e.target.result;
}
dbRequest.onupgradeneeded = function (e) {
	var thisDB = e.target.result;
	for (var i = 0; i < DbContext.tables.length; i++) {
		if (!thisDB.objectStoreNames.contains(DbContext.tables[i].name)) {
			thisDB.createObjectStore(DbContext.tables[i].name, DbContext.tables[i].schema);
		}
	}
}