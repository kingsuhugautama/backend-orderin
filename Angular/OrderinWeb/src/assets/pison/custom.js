//import { Swal } from "../sweetalert/sweetalert2@9";
var httpClient;

//penjagaan ctrl+u,ctrl+shift+i,f12,right click

Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + parseInt(days));
    return this;
};


document.onkeydown = function (e) {
    if (e.ctrlKey &&
        (e.keyCode === 85)) {
            //alert('not allowed');
            return false;
        } else {
            return true;
        }
    };
    
    function startTime(htmlid = 'waktuSekarang') {
        var today = new Date();
        var h = today.getHours();
        var m = today.getMinutes();
        var s = today.getSeconds();
        h = checkTime(h);
        m = checkTime(m);
        s = checkTime(s);
        document.getElementById(htmlid).innerHTML = h + ":" + m + ":" + s;
        var t = setTimeout(startTime, 500);
    }
    function checkTime(i) {
        if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
        return i;
    }
    
    function updateGridRecord(args, grid = document.getElementById("Grid").ej2_instances[0]) {
        var data = args.data;
        var index = args.rowIndex;
        grid.dataSource[index] = data;
        grid.refresh();
    }
    
    function setGridLocale(grids = []) {
        if (grids.length > 0) {
            for (let i = 0; i < grids.length; i++) {
                grids[i].locale = 'id';
            }
        }
    }
    function setDatePickerAreaLocale(count = 1) {
        if (count > 0) {
            for (let i = 1; i <= count; i++) {
                try {
                    datepicker = document.getElementById('datepicker' + i.toString()).ej2_instances[0];
                    grid = document.getElementById('datepicker' + i.toString()).ej2_instances[0];
                    datepicker.locale = 'id';
                } catch (err) {
                    
                }
            }
        }
    }
    
    $(window).on('keydown', function (event) {
        //if (event.keyCode == 123) {
        //    //alert('Entered F12');
        //    return false;
        //}else 
        if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
            //alert('Entered ctrl+shift+i')
            return false;  //Prevent from ctrl+shift+i
        }
        else if (event.ctrlKey && event.keyCode == 73) {
            //alert('Entered ctrl+shift+i')
            return false;  //Prevent from ctrl+shift+i
        }
    });
    
    $(document).on("contextmenu", function (e) {
        //alert('Right Click Not Allowed')
        e.preventDefault();
    });
    
    
    function getNumericValue(htmlid) {
        return $('#' + htmlid).attr("aria-valuenow");
    }
    
    function setNumericValueNoInstance(htmlid, value = 0) {
        
        $('#' + htmlid).siblings('.e-clear-icon').trigger('click');
        $('#' + htmlid).val(value);
        $('#' + htmlid).attr("aria-invalid", false);
        $('#' + htmlid).attr("aria-valuenow", value);
        
        $('input[name="' + htmlid + '"]').val(value);
        
        $('#' + htmlid).focus();
        $('#' + htmlid).blur();
        $('#' + htmlid).change();
    }
    
    function setNumericValue(htmlid, value = 0) {
        var closest = $('.e-clear-icon ').closest(htmlid).prevObject.length - 1;
        //console.log($('#' + htmlid).siblings('.e-clear-icon'));
        $('#' + htmlid).siblings('.e-clear-icon').trigger('click');
        //console.log(htmlid, value);
        $('#' + htmlid).val(value);
        
        $('#' + htmlid).attr("aria-invalid", false);
        $('#' + htmlid).attr("aria-valuenow", value);
        
        $('input[name="' + htmlid + '"]').val(value);
        //console.log(htmlid);
        //$('#' + htmlid).trigger('change');
        //$('input[name="' + htmlid + '"]').trigger('change');
        $('#' + htmlid).focus();
        $('#' + htmlid).blur();
        $('#' + htmlid).change();
    }
    
    function rowPositionChange(args) {
        var grid = document.getElementById("Grid").ej2_instances[0];
        grid.editSettings.newRowPosition = this.value;
    }
    
    function onOverlayClick() {
        var dialog = document.getElementById("dialog").ej2_instances[0];
        dialog.hide();
    }
    
    function closeDialog() {
        var dialog = document.getElementById("dialog").ej2_instances[0];
        dialog.hide();
    }
    
    
    function openDialog() {
        //document.getElementById('opendialog').onclick = function () {
        var dialog = document.getElementById("dialog").ej2_instances[0];
        
        dialog.position = {
            X: document.getElementById("content").offsetWidth * 40 / 100,
            Y: 100
        };
        
        
        dialog.height = "auto";
        dialog.show();
        //}
    }
    
    function getJsonAttrCount(obj) {
        var count = 0;
        for (var prop in obj) {
            if (obj.hasOwnProperty(prop)) {
                ++count;
            }
        }
        return count;
    }
    
    
    function swalConfirmSubmitWithParams(params, customText = "menyimpan") {
        Swal.fire({
            title: 'Perhatian',
            icon: 'question',
            text: 'Anda yakin ingin ' + customText + "?",
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            focusCancel: true,
            confirmButtonText:
            'Ya',
            showLoaderOnConfirm: true,
            preConfirm: () => {
                swalConfirmParams(params);
            },
            //confirmButtonAriaLabel: 'Thumbs up, great!',
            cancelButtonText:
            'Tidak'
            //cancelButtonAriaLabel: 'Thumbs down'
        })
    }
    
    
    function swalConfirmSubmit(customText = "menyimpan") {
        Swal.fire({
            title: 'Perhatian',
            icon: 'question',
            text: 'Anda yakin ingin ' + customText + "?",
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            focusCancel: true,
            confirmButtonText:
            'Ya',
            showLoaderOnConfirm: true,
            preConfirm: () => {
                swalConfirm();
            },
            //confirmButtonAriaLabel: 'Thumbs up, great!',
            cancelButtonText:
            'Tidak'
            //cancelButtonAriaLabel: 'Thumbs down'
        })
    }
    
    function generateNamaBarangNoValidate(htmls = [], htmlPassid = []) {
        
        var namaBarang = "";
        if (htmls.length > 0) {
            for (let i = 0; i < htmls.length; i++) {
                var formValueText = $('#' + htmls[i] + 'Text').val();
                //console.log(formValueText);
                if (i != 0) {
                    namaBarang += ' ' + formValueText.trim();
                } else {
                    namaBarang += formValueText.trim();
                }
            }
            
            if (htmlPassid.length > 0) {
                //for (let i = 0; i < htmls.length; i++) {
                $('#' + htmlPassid[0]).val(namaBarang);
                //}
            }
        }
    }
    function validationEmptyForm(htmls = [], htmlPassid = []) {
        var formValue = "";
        var kodeBarang = "";
        var namaBarang = "";
        if (htmls.length > 0) {
            for (let i = 0; i < htmls.length; i++) {
                //console.log(htmls[i]);
                var formValue = $('#' + htmls[i]).val();
                var formValueText = $('#' + htmls[i] + 'Text').val();
                if (!formValue) {
                    ////console.log(i);
                    alert('Harap isi ' + convertToLabelText(htmls[i]) + '!');
                    //Swal.fire({
                    //    icon:'error',
                    //    title: "Perhatian",
                    //    text: "Harap isi " + convertToLabelText(htmls[i]),
                    //    confirmButtonColor: "#DD6B55",
                    //    confirmButtonText: "OK"
                    //}).then(function (confirm) {
                    //    if (confirm) {
                    //        $('#' + htmls[i]).focus();
                    //        i = htmls.length;
                    //        return false;
                    //    }
                    //});
                    //return false;
                    $('#' + htmls[i]).focus();
                    i = htmls.length;
                } else {
                    if (i != 0) {
                        kodeBarang += '.' + formValue.trim();
                        if (htmls[i].toLowerCase().indexOf('barang') > -1) {
                            if (htmls[i].toLowerCase().indexOf('lebar') <= -1 && htmls[i].toLowerCase().indexOf('klasifikasi') <= -1) //lebar tidak usah diinclude ke nama barang
                            namaBarang += ' ' + formValueText.trim();
                        } else {
                            namaBarang += ' ' + formValueText.trim();
                        }
                    } else {
                        kodeBarang += formValue.trim();
                        namaBarang += formValueText.trim();
                    }
                }
                
            }
            
            if (htmlPassid.length > 0) {
                //for (let i = 0; i < htmls.length; i++) {
                $('#' + htmlPassid[0]).val(kodeBarang);
                $('#' + htmlPassid[1]).val(namaBarang);
                //}
            }
        }
    }
    
    function KodePanjang(value) {
        
        var pecahan = value.split(",");
        //console.log(pecahan);
        var hasil = "";
        hasil = parseInt(value).leadingZero(4);
        //if (pecahan.length > 1) {
        //    //console.log(parseInt(pecahan[0]).leadingZero(2) + pecahan[1].laggingZero(2));
        //    hasil = parseInt(pecahan[0]).leadingZero(2) + pecahan[1].laggingZero(2);
        
        //} else {
        //    //console.log(pecahan[0].laggingZero(4));
        //    hasil = pecahan[0].laggingZero(4);
        //}
        
        return hasil;
    }
    
    String.prototype.laggingZero = function (size) {
        var s = this;
        console.log(s.length);
        while (s.length < (size || 2)) { s = s + "0"; }
        return s;
    }
    
    Number.prototype.leadingZero = function (size) {
        var s = String(this);
        while (s.length < (size || 2)) { s = "0" + s; }
        return s;
    }
    
    function changeFocus(defaultFocus = 1, customText = "menyimpan", isSubmit = false) {
        
        $('[tabindex="' + defaultFocus + '"]').focus();
        $('button,input,select').on('keyup', function (e) {
            ////console.log(e.keyCode);
            if (e.keyCode == 13) {
                e.preventDefault();
                var nextElement = $('[tabindex="' + (this.tabIndex + 1) + '"]');
                nextElement.focus();
                //var inputs = $(this).closest('form').find(':input:visible');
                //inputs.eq(inputs.index(this) + 1).focus();
            }
        });
        
        $('#btnSubmit').click(function () {
            Swal.fire({
                title: 'Perhatian',
                icon: 'question',
                text: 'Anda yakin ingin ' + customText + "?",
                showCloseButton: true,
                showCancelButton: true,
                focusConfirm: false,
                focusCancel: true,
                confirmButtonText:
                'Ya',
                showLoaderOnConfirm: true,
                preConfirm: () => {
                    
                    
                    if (isSubmit == false) {
                        $('form').submit();
                    } else {
                        //alert('tes');
                        submitForm();
                    }
                },
                //confirmButtonAriaLabel: 'Thumbs up, great!',
                cancelButtonText:
                'Tidak'
                //cancelButtonAriaLabel: 'Thumbs down'
            })
            //$('form').submit();
        });
        
    }
    
    function clearform() {
        $('input').val('');
        $('input').prop('readonly', false);
    }
    
    function refreshGrid(grid = document.getElementById("Grid").ej2_instances[0]) {
        grid.refresh();//referesh the Grid data
        
        //gridObj.dataSource(ej.parseJSON(data)); 
    }
    
    function toolbarClick(args) {
        var action = args.item.id;
        //console.log(action);
        if (action == "Grid_add" || action == "Grid_edit") {
            callPartialView();
        }
    }
    
    function splitString(value) {
        
        return value.split(".");
    }
    
    function resetFormField(callbackPassId = []) {
        if (callbackPassId.length > 0) {
            for (let i = 0; i < callbackPassId.length; i++) {
                $('#' + callbackPassId[i].htmlid).val('');
                $('#' + callbackPassId[i].htmlid).html('');
                setNumericValueNoInstance(callbackPassId[i].htmlid, '');
                
            }
        }
    }

    function toProperCase(o) {
        var newO, origKey, newKey, value
        if (o instanceof Array) {
            return o.map(function (value) {
                if (typeof value === "object") {
                    value = toProperCase(value)
                }
                return value
            })
        } else {
            newO = {}
            for (origKey in o) {
                if (o.hasOwnProperty(origKey)) {
                    newKey = (origKey.charAt(0).toUpperCase() + origKey.slice(1) || origKey).toString()
                    value = o[origKey]
                    if (value instanceof Array || (value !== null && value.constructor === Object)) {
                        value = toProperCase(value)
                    }
                    newO[newKey] = value
                }
            }
        }
        return newO
    }
    
    function setTimePickerValuesById(setId, value) {
        $('#' + setId).val(value);
    }
    function setCalendarValuesById(setId, value) {
        
        //console.log(reformatingDateValue(value));
        var calendar = document.getElementById(setId).ej2_instances[0];
        $('#' + setId).val(ddMMMMyyyy(reformatingDateValue(value)));
        calendar.value = new Date(value);
        //console.log(calendar.value)
    }
    function setCalendarValues(args, setId) {
        var tgl = args.value;
        //console.log(args);
        
        var myDate = new Date(tgl);
        var d = myDate.getDate();
        var m = myDate.getMonth();
        m += 1;
        if (m < 10) {
            m = '0' + m;
        }
        if (d < 10) {
            d = '0' + d;
        }
        var y = myDate.getFullYear();
        
        var newdate = (y + "-" + m + "-" + d);
        ////console.log(newdate);
        $('#' + setId).val(newdate);
    }
    
    function ddMMMMyyyy(value) {
        var newdateddMMMMyyyy = moment(reformatingDateValue(value));
        
        return newdateddMMMMyyyy.format("DD MMMM YYYY");
    }
    
    function reformatingDateDMYtoYMD(date) {
        //console.log(date.split("/").reverse().join("-"));
        ////console.log(date.split("/").reverse().join("-"));
        return date.split("/").reverse().join("-");
    }
    
    function getCurrentDateISO(){
        var datetime = moment(new Date().toString()).format("YYYY-MM-DD HH:mm:ss").replace(" ","T");
        return datetime;
    }

    function getDateTime() {
        var datetime = moment(new Date().toString()).format("YYYY-MM-DD HH:mm:ss");
        return datetime;
    }
    function getTime() {
        var time = moment(new Date().toString()).format("HH:mm:ss");
        return time;
    }
    function getDateYMD() {
        var todayDate = moment(new Date().toString()).format("YYYY-MM-DD");
        return todayDate;
    }
    
    function getDateDMY(separatorFrom = "-", separatorTo = "/") {
        var todayDate = moment(new Date().toString()).format("YYYY-MM-DD").split(separatorFrom).reverse().join(separatorTo); //new Date().toISOString().slice(0, 10).toString().split(separatorFrom).reverse().join(separatorTo);
        return todayDate;
    }
    
    function getLastNoUrut(grid = document.getElementById("Grid").ej2_instances[0]) {
        var ds = grid.dataSource;
        var lastIndex = ds.length - 1;
        
        return lastIndex;
    }
    
    function toNumber(stringNumber) {
        //console.log(stringNumber);
        var number = stringNumber.toString() == "" ? 0 : stringNumber.toString();
        return parseFloat(number.replace(/[,"]+/g, '')).toFixed(2);
    }
    
    function reformatingDateValueById(id = "datepicker1") {
        var dateValue = document.getElementById(id).ej2_instances[0].value;
        var myDate = new Date(dateValue);
        var d = myDate.getDate();
        var m = myDate.getMonth();
        m += 1;
        if (m < 10) {
            m = '0' + m;
        }
        if (d < 10) {
            d = '0' + d;
        }
        var y = myDate.getFullYear();
        
        
        var newdate = "";
        
        if (dateValue && dateValue != "NaN-NaN-NaN") {
            newdate = (y + "-" + m + "-" + d);
        } else if (dateValue == "NaN-NaN-NaN") {
            newdate = "1900-01-01";
        } else {
            newdate = "1900-01-01";
        }
        //console.log(newdate);
        return newdate;
    }
    
    function reformatingDateValue(dateValue) {
        //console.log(dateValue);
        var tgl = dateValue;
        var myDate = new Date(tgl);
        var d = myDate.getDate();
        var m = myDate.getMonth();
        m += 1;
        if (m < 10) {
            m = '0' + m;
        }
        if (d < 10) {
            d = '0' + d;
        }
        var y = myDate.getFullYear();
        
        
        var newdate = "";
        
        if (dateValue && dateValue != "NaN-NaN-NaN") {
            newdate = (y + "-" + m + "-" + d);
        } else if (dateValue == "NaN-NaN-NaN") {
            newdate = "1900-01-01";
        } else {
            newdate = "1900-01-01";
        }
        //console.log(newdate);
        return newdate;
    }
    
    function setComboboxReadonly(id, status) {
        document.getElementById(id).ej2_instances[0].readonly = status;
    }
    
    function setComboBoxValuesText(args, setId) {
        //console.log(args);
        
        var value = args.itemData.Nama;
        $('#' + setId).val(value);
    }
    function setComboBoxValues(args, setId) {
        //console.log(args);
        var value = args.value;
        $('#' + setId).val(value);
    }
    
    function disableMouseDragOrSwiping(htmlclassorid = []) {
        
        if (htmlclassorid.length > 0) {
            for (let i = 0; i < htmlclassorid.length; i++) {
                $(htmlclassorid[i]).mousedown(function () { return false });
            }
        }
    }
    
    function setDropdownListValueByDescNumber(comboboxId, key, comboboxKetId = "") {
        var dropdown = document.getElementById(comboboxId).ej2_instances[0]
        
        ////console.log(dropdown.listData);
        var filter = dropdown.listData.filter((df) => {
            return df["Nama"] == (parseFloat(key) + 0.05).toString();
        });
        ////console.log(filter);
        
        if (filter.length > 0) {
            $('#' + comboboxId).val(filter[0].Id);
            $('#' + comboboxKetId).val(filter[0].Text);
            
            var option = '<option value = "' + filter[0].Id + '" selected >' + filter[0].Nama + '</option>'
            $('select[name="' + comboboxId + '"]').append(option);
        }
        //var i = data.filter((df) => {
        //    return df[Object.keys(df)[0]] == items[Object.keys(items)[0]];
        //});
    }
    
    function setDropdownListValueByDesc(comboboxId, key, comboboxKetId = "") {
        var dropdown = document.getElementById(comboboxId).ej2_instances[0]
        
        //console.log(dropdown.listData);
        var filter = dropdown.listData.filter((df) => {
            return df["Nama"] == key;
        });
        ////console.log(filter);
        
        if (filter.length > 0) {
            dropdown.value = filter[0].Id;
            //$('#' + comboboxId).val(filter[0].Id);
            $('#' + comboboxKetId).val(filter[0].Text);
            
            var option = '<option value = "' + filter[0].Id + '" selected >' + filter[0].Nama + '</option>'
            $('select[name="' + comboboxId + '"]').append(option);
        }
        //var i = data.filter((df) => {
        //    return df[Object.keys(df)[0]] == items[Object.keys(items)[0]];
        //});
    }
    function setComboboxValueById(comboboxId, key, comboboxKetId = "") {
        var dropdown = document.getElementById(comboboxId).ej2_instances[0]
        
        var filter = dropdown.listData.filter((df) => {
            return df["Id"] == key;
        });
        //console.log(filter);
        
        if (filter.length > 0) {
            $('select[name="' + comboboxId + '"] option[value="' + filter[0].Id + '"]').remove();
            
            var option = '<option selected value = "' + filter[0].Id + '" >' + filter[0].Nama + '</option>'
            $('select[name="' + comboboxId + '"]').append(option);
            $('#' + comboboxId).val(filter[0].Nama);
            dropdown.value = filter[0].Id;
            if (comboboxKetId) {
                $('#' + comboboxKetId).val(filter[0].Text);
            }
        } else {
            $('select[name="' + comboboxId + '"]').find('option:selected').remove().end();
            $('#' + comboboxId).val('');
        }
        //var i = data.filter((df) => {
        //    return df[Object.keys(df)[0]] == items[Object.keys(items)[0]];
        //});
    }
    
    function setDropdownListValue(comboboxId, key) {
        var dropdown = document.getElementById(comboboxId).ej2_instances[0]
        //console.log(dropdown);
        
        //console.log(filter);
        
        if (key) {
            var filter = dropdown.dataSource.filter((df) => {
                return df["Id"] == key;
            });
            
            if (filter.length > 0) {
                //console.log(filter);
                $('select[name="' + comboboxId + '"] option[value="' + filter[0].Id + '"]').remove();
                
                var option = '<option selected value = "' + filter[0].Id + '" >' + filter[0].Nama + '</option>'
                $('select[name="' + comboboxId + '"]').append(option);
                //$('#' + comboboxId).val(filter[0].Id);
                
                dropdown.value = filter[0].Id;
            }
        } else {
            dropdown.value = null;
        }
    }
    
    function setComboboxValueToIdById(comboboxId, key, comboboxKetId = "") {
        var dropdown = document.getElementById(comboboxId).ej2_instances[0]
        //console.log(dropdown);
        var filter = dropdown.listData.filter((df) => {
            return df["Id"] == key;
        });
        
        //console.log(filter);
        
        if (filter.length > 0) {
            $('#' + comboboxId).val(filter[0].Id);
            $('#' + comboboxKetId).val(filter[0].Text);
        }
    }
    function setDropdownListValueById(comboboxId, key, comboboxKetId = "") {
        var dropdown = document.getElementById(comboboxId).ej2_instances[0]
        //console.log(dropdown);
        var filter = dropdown.listData.filter((df) => {
            return df["Id"] == key;
        });
        //console.log(filter);
        
        if (filter.length > 0) {
            $('select[name="' + comboboxId + '"] option[value="' + filter[0].Id + '"]').remove();
            
            var option = '<option selected value = "' + filter[0].Id + '" >' + filter[0].Nama + '</option>'
            $('select[name="' + comboboxId + '"]').append(option);
            //$('#' + comboboxId).val(filter[0].Id);
            dropdown.value = filter[0].Id;
            if (comboboxKetId) {
                $('#' + comboboxKetId).val(filter[0].Text);
            }
        }
        //var i = data.filter((df) => {
        //    return df[Object.keys(df)[0]] == items[Object.keys(items)[0]];
        //});
    }
    
    function leading(num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }
    
    function setDropdownListValueEditing(htmlid, valueSelect, htmlText, valueText, swapValue = false) {
        if (swapValue) {
            $('#' + htmlid).val(valueText);
            $('#' + htmlText).val(valueSelect);
        } else {
            $('#' + htmlid).val(valueSelect);
            $('#' + htmlText).val(valueText);
        }
        
        var option = '<option value = "' + valueSelect + '" selected >' + valueText + '</option>'
        $('select[name="' + htmlid + '"]').append(option);
    }
    
    function setComboboxValueMask(args) {
        ////console.log(args.item);
        
        var readonly = $('#' + args.element.id).attr('readonly');
        ////console.log(readonly);
        
        if (readonly != "readonly") {
            if (args.item) {
                $('#' + args.element.id).val(args.value);
                $('#' + args.element.id + 'Text').val(args.itemData.Text);
                //validate(); //bikin fungsi validate yang memanggil fungsi validationEmptyForm(parameter)
            } else {
                $('#' + args.element.id + 'Text').val('');
            }
        }
    }
    
    function getGridColumn(colid, grid = document.getElementById("Grid").ej2_instances[0]) {
        //var grid = document.getElementById("Grid").ej2_instances[0];
        var result = grid.columns.filter(function (d) {
            return d["headerText"] == colid;
        });
        
        return result;
    }
    
    
    function getFormDataArrayColumn(data, colid) {
        
        var result = data.filter(function (d) {
            return d["name"] == colid;
        });
        return result;
    }
    
    function convertToLabelText(header) {
        var gridHeader = header.replace(/([A-Z])/g, ' $1').trim();
        return gridHeader;
    }
    function convertToHeaderText(header) {
        var gridHeader = header.replace(/([A-Z])/g, ' $1').trim();
        return gridHeader.toUpperCase();
    }
    
    function formToJson(form) {
        var config = {};
        form.serializeArray().map(function (item) {
            ////console.log(isNaN(item.value));
            if (!config.hasOwnProperty(item.name)) {
                if (config[item.name]) {
                    if (typeof (config[item.name]) === "string") {
                        config[item.name] = [config[item.name]];
                    }
                    
                    if (item.value == "true")
                    item.value = true;
                    else if (item.value == "false")
                    item.value = false;
                    
                    config[item.name].push(item.value);
                } else {
                    if (item.value == "true")
                    item.value = true;
                    else if (item.value == "false")
                    item.value = false;
                    
                    config[item.name] = item.value;
                }
            }
        });
        
        //if (item.value) {
        //    if (isNaN(item.value)) {
        //        if (item.value == "true")
        //            item.value = true;
        //        else if (item.value == "false")
        //            item.value = false;
        //    } else {
        //        item.value = parseFloat(item.value);
        //    }
        //}
        ////console.log(config);
        
        return config;
    }
    
    function renameGridHeader(grid) {
        for (let i = 0; i < grid.columns.length; i++) {
            getGridColumn(grid.columns[i].field, grid)[0].headerText = convertToHeaderText(getGridColumn(grid.columns[i].field, grid)[0].headerText);
            grid.refresh();
        }
    }
    
    function swalMsg(msg = '', title = 'Perhatian', icon = 'error') {
        Swal.fire({
            title: title,
            icon: icon,
            text: msg
        });
    }
    
    function TreeViewSelectCheckbox(args, tree = document.getElementById("treedata").ej2_instances[0]) {
        var tree = document.getElementById("treedata").ej2_instances[0];
        //console.log(args);
        var checkedNode = [args.node];
        if (args.event.target.classList.contains('e-fullrow') || args.event.key == "Enter") {
            var getNodeDetails = tree.getNodeData(args.node);
            if (getNodeDetails.isChecked == 'true') {
                tree.uncheckAll(checkedNode);
            } else {
                tree.checkAll(checkedNode);
            }
        }
    }
    function reload() {
        location.reload();
    }
    
    function collapseSidebar(menuItems) {
        if ($('#sidebar-menu').hasClass('e-open')) {
            $('#partial-menu').html(menuItems);
        } else {
            $('#partial-menu').html(menuItems);
            SidebarCollapse(); //ketika di tampilan collapse maka menampilkan icon nya saja
        }
    }
    
    function findButton(classBody = "body", equal = "", trigger = "click") {
        $(classBody).find('button').each(function () {
            var button = $(this).text().trim().toLowerCase();
            if (button == equal) {
                $(this).trigger(trigger);
            }
        });
    }
    
    function enableReadOnlyElement(htmlid) {
        $('#' + htmlid).prop('readonly', true);
    }
    
    function disableReadOnlyElement(htmlid) {
        $('#' + htmlid).prop('readonly', false);
    }
    
    function clearValueElement(htmlid) {
        $('#' + htmlid).val('');
        $('#' + htmlid).html('');
    }
    function disableElement(htmlid) {
        $('#' + htmlid).prop('disabled', true);
    }
    function enableElement(htmlid) {
        $('#' + htmlid).prop('disabled', false);
    }
    
    function disableGridDialogButtonByDivClassFocus(htmlFormId = [], divClass = "e-dialog", classButton = ".e-primary", ignoreZero = true) {
        //formValidation = ["","",""]
        $("button" + classButton).prop('disabled', true);
        
        $('.' + divClass).on('focusin', function (s) {
            
            if (htmlFormId.length > 0) {
                for (let i = 0; i < htmlFormId.length; i++) {
                    var value = $('#' + htmlFormId[i]).val();
                    //console.log(value);
                    if (ignoreZero) {
                        if (value) {
                            //do nothing
                            $("button" + classButton).prop('disabled', false);
                        } else {
                            $("button" + classButton).prop('disabled', true);
                            i = htmlFormId.length - 1;
                        }
                    } else {
                        if (value && parseInt(value) != 0) {
                            //do nothing
                            
                            //console.log('a');
                            $("button" + classButton).prop('disabled', false);
                        } else {
                            //console.log('b');
                            $("button" + classButton).prop('disabled', true);
                            i = htmlFormId.length - 1;
                        }
                    }
                }
            }
        });
        
        
        $('.' + divClass).on('focusout', function (s) {
            
            if (htmlFormId.length > 0) {
                for (let i = 0; i < htmlFormId.length; i++) {
                    var value = $('#' + htmlFormId[i]).val();
                    
                    if (ignoreZero) {
                        if (value) {
                            //do nothing
                            $("button" + classButton).prop('disabled', false);
                        } else {
                            $("button" + classButton).prop('disabled', true);
                            i = htmlFormId.length - 1;
                        }
                    } else {
                        
                        //alert('not ignored');
                        //alert(value)
                        if (value && parseInt(value) != 0) {
                            //do nothing
                            $("button" + classButton).prop('disabled', false);
                        } else {
                            $("button" + classButton).prop('disabled', true);
                            i = htmlFormId.length - 1;
                        }
                    }
                }
            }
        });
    }
    
    function disableGridDialogButton(formValidation = [], className = ".e-primary", ignoreZero = true) {
        //formValidation = ["","",""]
        $("button" + className).prop('disabled', true);
        //console.log(formValidation.length);
        if (formValidation.length > 0) {
            for (let i = 0; i < formValidation.length; i++) {
                $('input[id="' + formValidation[i] + '"],input[id="' + formValidation[i] + '_hidden"],textarea[id="' + formValidation[i] + '"]').on('focusout', function (s) {
                    
                    for (let j = 0; j < formValidation.length; j++) {
                        var value = $('#' + formValidation[j]).val();
                        
                        //console.log(value);
                        if (ignoreZero) {
                            //alert('ignored');
                            if (value) {
                                //do nothing
                                $("button" + className).prop('disabled', false);
                            } else {
                                $("button" + className).prop('disabled', true);
                                j = formValidation.length - 1;
                            }
                        } else {
                            
                            //alert('not ignored');
                            
                            //console.log(value);
                            
                            //console.log(isNaN(parseInt(value)));
                            
                            if (!isNaN(parseInt(value))) {
                                if (value && parseInt(value) > 0) {
                                    //do nothing
                                    $("button" + className).prop('disabled', false);
                                    //console.log(formValidation[j], 'enabled');
                                } else {
                                    $("button" + className).prop('disabled', true);
                                    j = formValidation.length - 1;
                                    //console.log(formValidation[j], 'disabled');
                                }
                            } else {
                                if (value) {
                                    //do nothing
                                    $("button" + className).prop('disabled', false);
                                    //console.log(formValidation[j], 'enabled');
                                } else {
                                    $("button" + className).prop('disabled', true);
                                    j = formValidation.length - 1;
                                    //console.log(formValidation[j], 'disabled');
                                }
                            }
                        }
                    }
                    
                });
            }
        }
    }
    
    
    function enableDialogButton() {
        $("button.e-primary").prop('disabled', false);
    }
    function disableDialogButton() {
        $("button.e-primary").prop('disabled', true);
    }
    function gridDeleteRecordsClientSide(args, deleteKey = "", grid = document.getElementById("Grid").ej2_instances[0]) {
        //console.log(grid);
        var ds = JSON.parse(JSON.stringify(grid.dataSource));
        
        if (deleteKey) {
            var filter = ds.filter((ds) => {
                return ds[deleteKey] != args.data[0][deleteKey];
            });
            
            //console.log(filter);
            grid.dataSource = filter;
        } else {
            var index = args.tr[0].rowIndex;
            ds.splice(index, 1);
            grid.dataSource = ds;
        }
        
        grid.refresh();
    }
    
    function UpperCaseForm() {
        //SEMUA FORM MENJADI HURUF BESAR
        $('input[type="text"],textarea').on('focusout', function (s) {
            this.value = this.value.toUpperCase();
        });
    }
    
    function resetGrid(grid = document.getElementById("Grid").ej2_instances[0]) {
        grid.dataSource = [];
        grid.refresh();
    }
    
    
    function resetDataTable(tableid) {
        var table = $(tableid);
        if ($.fn.DataTable.isDataTable(table)) {
            table.dataTable().fnClearTable();
            table.dataTable().fnDestroy();
        }
    }
    
    function showModal(modal) {
        modal.appendTo("body");
        modal.modal("show");
    }
    
    
    function formatThousandSeparatorKomaNolNol(angka, prefix) {
        angka = angka.toString().replace(".", ",");
        var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);
        
        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? ',' : '';
            rupiah += separator + ribuan.join(',');
        }
        
        rupiah = split[1] != undefined ? rupiah + '.' + split[1] : rupiah;
        return prefix == undefined ? rupiah + ".00" : (rupiah ? prefix + rupiah + ".00" : '');
    }
    
    function formatThousandSeparator(angka, prefix) {
        angka = angka.toString().replace(".", ",");
        var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);
        
        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? ',' : '';
            rupiah += separator + ribuan.join(',');
        }
        
        rupiah = split[1] != undefined ? rupiah + '.' + split[1] : rupiah;
        return prefix == undefined ? rupiah : (rupiah ? prefix + rupiah : '');
    }
    
    
    function formatRupiah(angka, prefix) {
        angka = angka.toString().replace(".", ",");
        //alert(angka);
        var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);
        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? '.' : '';
            rupiah += separator + ribuan.join('.');
        }
        
        //console.log(number_string, split,rupiah);
        rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
        return prefix == undefined ? rupiah : (rupiah ? prefix + rupiah : '');
    }
    
    function formatRupiahKomaNolNol(angka, prefix) {
        angka = angka.toString().replace(".", ",");
        //alert(angka);
        var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);
        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? '.' : '';
            rupiah += separator + ribuan.join('.');
        }
        
        //console.log(number_string, split,rupiah);
        rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
        return prefix == undefined ? rupiah + ",00" : (rupiah ? prefix + rupiah + ",00" : '');
    }
    
    function convertBase64ToFile(data,extension = "vnd.ms-excel") {
        //excel = vnd.ms-excel
        //pdf = pdf

        var contentType = 'application/' + extension;
        var blob1 = b64toBlob(data, contentType);
        var blobUrl1 = URL.createObjectURL(blob1);
        
        window.open(blobUrl1);
    }
    
    function convertBase64ToExcel(data) {
        
        var contentType = 'application/vnd.ms-excel';
        var blob1 = b64toBlob(data, contentType);
        var blobUrl1 = URL.createObjectURL(blob1);
        
        window.open(blobUrl1);
    }
    
    function b64toBlob(b64Data, contentType, sliceSize) {
        contentType = contentType || '';
        sliceSize = sliceSize || 512;
        
        var byteCharacters = atob(b64Data);
        var byteArrays = [];
        
        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            var slice = byteCharacters.slice(offset, offset + sliceSize);
            
            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }
            
            var byteArray = new Uint8Array(byteNumbers);
            
            byteArrays.push(byteArray);
        }
        
        var blob = new Blob(byteArrays, { type: contentType });
        return blob;
    };
    
    
    function fullScreenElement(htmlid) {
        var elem = document.getElementById(htmlid);
        
        if (elem.requestFullscreen) {
            elem.requestFullscreen();
        } else if (elem.mozRequestFullScreen) { /* Firefox */
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
            elem.webkitRequestFullscreen();
        } else if (elem.msRequestFullscreen) { /* IE/Edge */
            elem.msRequestFullscreen();
        }
    }
    
    function exitFullScreen() {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) { /* Firefox */
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) { /* Chrome, Safari and Opera */
            document.webkitExitFullscreen();
        } else if (document.msExitFullscreen) { /* IE/Edge */
            document.msExitFullscreen();
        }
    }
    
    function replaceEmptyJsonArrayWithNulls(json){
        return JSON.parse(JSON.stringify(json).replace(/\[]+/g,null));
    }
    
    function replaceJsonPropertyName(json,name,replaceWith){
        var replace = name;
        var re = new RegExp(replace,"g");
        return JSON.stringify(json).replace(re,replaceWith);
    }


    function readNotification(http,url_api,redirect_url,notifId,userId){
        
        http.post(url_api,{
            'IdNotification':notifId,
            'UserId':userId
          }).subscribe((data)=>{
            // console.log(data);
            location.href = redirect_url;
          });
    }

