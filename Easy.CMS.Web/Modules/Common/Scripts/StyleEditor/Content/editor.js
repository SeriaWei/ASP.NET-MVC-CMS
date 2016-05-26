function updateDisplay() {

    var width = document.getElementById("width").value;
    var height = document.getElementById("height").value;

    var textColor = document.getElementById("t-color").value;
    var backgroundColor = document.getElementById("b-color").value;

    //Border//
    var borderWidth = document.getElementById("border-width").innerHTML + "px";
    var borderColor = document.getElementById("bc-color").value;
    var borderStyle = document.getElementById("border-style").value;

    //Padding & Margins//
    var padding = document.getElementById("padding-custom").value;
    if (!padding) {
        padding = document.getElementById("padding").innerHTML + "px";
    }
    var margin = document.getElementById("margin-custom").value;
    if (!margin) {
        margin = document.getElementById("margin").innerHTML + "px";
    }

    //Fonts//
    var fontName = document.getElementById("font-name").value;
    var fontStyle = document.getElementById("font-style").value;
    var fontWeight = document.getElementById("font-weight").value;
    var fontSize = document.getElementById("font-size").innerHTML + "px";
    var fontVariant = document.getElementById("font-variant").value;
    var lineHeight = document.getElementById("line-height").innerHTML + "px";

    //Text Style//
    var textAlign = document.getElementById("text-align").value;
    var textDecoration = document.getElementById("text-decoration").value;
    var textIndent = document.getElementById("text-indent").innerHTML + "px";
    var letterSpacing = document.getElementById("letter-spacing").innerHTML + "px";
    var wordSpacing = document.getElementById("word-spacing").innerHTML + "px";
    var textTransform = document.getElementById("text-transform").value;

    //Background Styles//
    var backgroundImage = document.getElementById("background-image").value;
    if (backgroundImage) {
        backgroundImage = 'url(' + backgroundImage.replace("~/", "/") + ')';
    }
    var backgroundRepeat = document.getElementById("background-repeat").value;
    var backgroundPosition = document.getElementById("background-position").value;
    var backgroundAttachment = document.getElementById("background-attachment").value;

    //Position Style//
    var cssPosition = document.getElementById("position").value;
    var cssTop = document.getElementById("top").innerHTML + "px";
    var cssLeft = document.getElementById("left").innerHTML + "px";
    var cssRight = document.getElementById("right").innerHTML + "px";
    var cssBottom = document.getElementById("bottom").innerHTML + "px";

    //Extras//
    var cssCursor = document.getElementById("cursor").value;
    var cssVisibility = document.getElementById("visibility").value;
    var cssOverflow = document.getElementById("overflow").value;
    var cssFloat = document.getElementById("float").value;

    //CSS3 Styles//
    var borderRadius = document.getElementById("border-radius").innerHTML + "px";
    var textShadowH = document.getElementById("text-h-length").innerHTML + "px";
    var textShadowV = document.getElementById("text-v-length").innerHTML + "px";
    var textShadowB = document.getElementById("text-b-length").innerHTML + "px";
    var textShadowColor = document.getElementById("text-s-color").value;
    var boxShadowH = document.getElementById("box-h-length").innerHTML + "px";
    var boxShadowV = document.getElementById("box-v-length").innerHTML + "px";
    var boxShadowB = document.getElementById("box-b-length").innerHTML + "px";
    var boxShadowColor = document.getElementById("box-s-color").value;




    this.css = '';
    this.css += 'background-color: ' + backgroundColor + ';\n';
    this.css += ' color: ' + textColor + ';\n';
    if (width) {
        this.css += ' width: ' + width + 'px;\n';
    }
    if (height) {
        this.css += ' height: ' + height + 'px;\n';
    }
    if (document.getElementById("border-width").innerHTML > "0") {
        this.css += ' border: ' + borderWidth + ' ' + borderStyle + ' ' + borderColor + ';\n';
    }
    if (document.getElementById("padding").innerHTML > "0") {
        this.css += ' padding: ' + padding + ';\n';
    }
    if (document.getElementById("margin").innerHTML > "0") {
        this.css += ' margin: ' + margin + ';\n';
    }
    if (document.getElementById("font-name").value != "") {
        this.css += ' font-family: ' + fontName + ';\n';
    }
    if (document.getElementById("font-style").value != "") {
        this.css += ' font-style: ' + fontStyle + ';\n';
    }
    if (document.getElementById("font-weight").value != "") {
        this.css += ' font-weight: ' + fontWeight + ';\n';
    }
    if (document.getElementById("font-size").innerHTML > "0") {
        this.css += ' font-size: ' + fontSize + ';\n';
    }
    if (document.getElementById("font-variant").value != "") {
        this.css += ' font-variant: ' + fontVariant + ';\n';
    }
    if (document.getElementById("line-height").innerHTML > "0") {
        this.css += ' line-height: ' + lineHeight + ';\n';
    }
    if (document.getElementById("text-align").value != "") {
        this.css += ' text-align: ' + textAlign + ';\n';
    }
    if (document.getElementById("text-decoration").value != "") {
        this.css += ' text-decoration: ' + textDecoration + ';\n';
    }
    if (document.getElementById("text-indent").innerHTML > "0") {
        this.css += ' text-indent: ' + textIndent + ';\n';
    }
    if (document.getElementById("letter-spacing").innerHTML > "0") {
        this.css += ' letter-spacing: ' + letterSpacing + ';\n';
    }
    if (document.getElementById("word-spacing").innerHTML > "0") {
        this.css += ' word-spacing: ' + wordSpacing + ';\n';
    }
    if (document.getElementById("text-transform").value != "") {
        this.css += ' text-transform: ' + textTransform + ';\n';
    }
    if (document.getElementById("background-image").value != "") {
        this.css += ' background-image: ' + backgroundImage + ';\n';
    }
    if (document.getElementById("background-repeat").value != "") {
        this.css += ' background-repeat: ' + backgroundRepeat + ';\n';
    }
    if (document.getElementById("background-position").value != "") {
        this.css += ' background-position: ' + backgroundPosition + ';\n';
    }
    if (document.getElementById("background-attachment").value != "") {
        this.css += ' background-attachment: ' + backgroundAttachment + ';\n';
    }
    if (document.getElementById("position").value != "") {
        this.css += ' position: ' + cssPosition + ';\n';
    }
    if (document.getElementById("top").innerHTML > "0") {
        this.css += ' top: ' + cssTop + ';\n';
    }
    if (document.getElementById("left").innerHTML > "0") {
        this.css += ' left: ' + cssLeft + ';\n';
    }
    if (document.getElementById("right").innerHTML > "0") {
        this.css += ' right: ' + cssRight + ';\n';
    }
    if (document.getElementById("bottom").innerHTML > "0") {
        this.css += ' bottom: ' + cssBottom + ';\n';
    }
    if (document.getElementById("cursor").value != "") {
        this.css += ' cursor: ' + cssCursor + ';\n';
    }
    if (document.getElementById("visibility").value != "") {
        this.css += ' visibility: ' + cssVisibility + ';\n';
    }
    if (document.getElementById("overflow").value != "") {
        this.css += ' overflow: ' + cssOverflow + ';\n';
    }
    if (document.getElementById("float").value != "") {
        this.css += ' float: ' + cssFloat + ';\n';
    }
    if (document.getElementById("border-radius").innerHTML > "0") {
        this.css += ' border-radius: ' + borderRadius + ';\n';
    }
    if (document.getElementById("text-h-length").innerHTML > "0" || document.getElementById("text-v-length").innerHTML > "0" || document.getElementById("text-b-length").innerHTML > "0") {
        this.css += ' text-shadow: ' + textShadowH + ' ' + textShadowV + ' ' + textShadowB + ' ' + textShadowColor + ';\n';
    }
    if (document.getElementById("box-h-length").innerHTML > "0" || document.getElementById("box-v-length").innerHTML > "0" || document.getElementById("box-b-length").innerHTML > "0") {
        this.css += ' box-shadow: ' + boxShadowH + ' ' + boxShadowV + ' ' + boxShadowB + ' ' + boxShadowColor + ';\n';
    }
    var target = window.top.$(".custom-style-target");
    if (target.hasClass('form-control')) {
        target.val("style=\"" + $.trim(this.css) + "\"");
    } else {
        target.attr("style", this.css);
    }

    this.css = "#cssDisplay {\n" + this.css + '}\n';

    codeDiv = document.getElementById("output");
    if (codeDiv.innerText) {
        codeDiv.innerText = this.css
    } else {
        codeDiv.textContent = this.css
    }

    $('style').remove();
    $('head').append('<style type="text/css">' + this.css + '</style>');
    return this.css;
}