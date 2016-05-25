function updateDisplay() {

    demoDiv = document.getElementById("target");
    textColor = document.getElementById("t-color").value;
    backgroundColor = document.getElementById("b-color").value;

    //Border//
    borderWidth = document.getElementById("border-width").innerHTML + "px";
    borderColor = document.getElementById("bc-color").value;
    borderStyle = document.getElementById("border-style").value;

    //Padding & Margins//
    Padding = document.getElementById("padding").innerHTML + "px";
    Margin = document.getElementById("margin").innerHTML + "px";

    //Fonts//
    fontName = document.getElementById("font-name").value;
    fontStyle = document.getElementById("font-style").value;
    fontWeight = document.getElementById("font-weight").value;
    fontSize = document.getElementById("font-size").innerHTML + "px";
    fontVariant = document.getElementById("font-variant").value;
    lineHeight = document.getElementById("line-height").innerHTML + "px";

    //Text Style//
    textAlign = document.getElementById("text-align").value;
    textDecoration = document.getElementById("text-decoration").value;
    textIndent = document.getElementById("text-indent").innerHTML + "px";
    letterSpacing = document.getElementById("letter-spacing").innerHTML + "px";
    wordSpacing = document.getElementById("word-spacing").innerHTML + "px";
    textTransform = document.getElementById("text-transform").value;

    //Background Styles//
    backgroundImage = document.getElementById("background-image").value;
    if (backgroundImage) {
        backgroundImage = 'url(' + backgroundImage + ')'
    }
    backgroundRepeat = document.getElementById("background-repeat").value;
    backgroundPosition = document.getElementById("background-position").value;
    backgroundAttachment = document.getElementById("background-attachment").value;

    //Position Style//
    cssPosition = document.getElementById("position").value;
    cssTop = document.getElementById("top").innerHTML + "px";
    cssLeft = document.getElementById("left").innerHTML + "px";
    cssRight = document.getElementById("right").innerHTML + "px";
    cssBottom = document.getElementById("bottom").innerHTML + "px";

    //Extras//
    cssCursor = document.getElementById("cursor").value;
    cssVisibility = document.getElementById("visibility").value;
    cssOverflow = document.getElementById("overflow").value;
    cssFloat = document.getElementById("float").value;

    //CSS3 Styles//
    borderRadius = document.getElementById("border-radius").innerHTML + "px";
    textShadowH = document.getElementById("text-h-length").innerHTML + "px";
    textShadowV = document.getElementById("text-v-length").innerHTML + "px";
    textShadowB = document.getElementById("text-b-length").innerHTML + "px";
    textShadowColor = document.getElementById("text-s-color").value;
    boxShadowH = document.getElementById("box-h-length").innerHTML + "px";
    boxShadowV = document.getElementById("box-v-length").innerHTML + "px";
    boxShadowB = document.getElementById("box-b-length").innerHTML + "px";
    boxShadowColor = document.getElementById("box-s-color").value;




    this.css = '';
    this.css += '  background-color: ' + backgroundColor + ';\n';
    this.css += '  color: ' + textColor + ';\n';
    if (document.getElementById("border-width").innerHTML > "0") {
        this.css += '  border: ' + borderWidth + ' ' + borderStyle + ' ' + borderColor + ';\n';
    }
    if (document.getElementById("padding").innerHTML > "0") {
        this.css += '  padding: ' + Padding + ';\n';
    }
    if (document.getElementById("margin").innerHTML > "0") {
        this.css += '  margin: ' + Margin + ';\n';
    }
    if (document.getElementById("font-name").value != "") {
        this.css += '  font-family: ' + fontName + ';\n';
    }
    if (document.getElementById("font-style").value != "") {
        this.css += '  font-style: ' + fontStyle + ';\n';
    }
    if (document.getElementById("font-weight").value != "") {
        this.css += '  font-weight: ' + fontWeight + ';\n';
    }
    if (document.getElementById("font-size").innerHTML > "0") {
        this.css += '  font-size: ' + fontSize + ';\n';
    }
    if (document.getElementById("font-variant").value != "") {
        this.css += '  font-variant: ' + fontVariant + ';\n';
    }
    if (document.getElementById("line-height").innerHTML > "0") {
        this.css += '  line-height: ' + lineHeight + ';\n';
    }
    if (document.getElementById("text-align").value != "") {
        this.css += '  text-align: ' + textAlign + ';\n';
    }
    if (document.getElementById("text-decoration").value != "") {
        this.css += '  text-decoration: ' + textDecoration + ';\n';
    }
    if (document.getElementById("text-indent").innerHTML > "0") {
        this.css += '  text-indent: ' + textIndent + ';\n';
    }
    if (document.getElementById("letter-spacing").innerHTML > "0") {
        this.css += '  letter-spacing: ' + letterSpacing + ';\n';
    }
    if (document.getElementById("word-spacing").innerHTML > "0") {
        this.css += '  word-spacing: ' + wordSpacing + ';\n';
    }
    if (document.getElementById("text-transform").value != "") {
        this.css += '  text-transform: ' + textTransform + ';\n';
    }
    if (document.getElementById("background-image").value != "") {
        this.css += '  background-image: ' + backgroundImage + ';\n';
    }
    if (document.getElementById("background-repeat").value != "") {
        this.css += '  background-repeat: ' + backgroundRepeat + ';\n';
    }
    if (document.getElementById("background-position").value != "") {
        this.css += '  background-position: ' + backgroundPosition + ';\n';
    }
    if (document.getElementById("background-attachment").value != "") {
        this.css += '  background-attachment: ' + backgroundAttachment + ';\n';
    }
    if (document.getElementById("position").value != "") {
        this.css += '  position: ' + cssPosition + ';\n';
    }
    if (document.getElementById("top").innerHTML > "0") {
        this.css += '  top: ' + cssTop + ';\n';
    }
    if (document.getElementById("left").innerHTML > "0") {
        this.css += '  left: ' + cssLeft + ';\n';
    }
    if (document.getElementById("right").innerHTML > "0") {
        this.css += '  right: ' + cssRight + ';\n';
    }
    if (document.getElementById("bottom").innerHTML > "0") {
        this.css += '  bottom: ' + cssBottom + ';\n';
    }
    if (document.getElementById("cursor").value != "") {
        this.css += '  cursor: ' + cssCursor + ';\n';
    }
    if (document.getElementById("visibility").value != "") {
        this.css += '  visibility: ' + cssVisibility + ';\n';
    }
    if (document.getElementById("overflow").value != "") {
        this.css += '  overflow: ' + cssOverflow + ';\n';
    }
    if (document.getElementById("float").value != "") {
        this.css += '  float: ' + cssFloat + ';\n';
    }
    if (document.getElementById("border-radius").innerHTML > "0") {
        this.css += '  border-radius: ' + borderRadius + ';\n';
    }
    if (document.getElementById("text-h-length").innerHTML > "0" || document.getElementById("text-v-length").innerHTML > "0" || document.getElementById("text-b-length").innerHTML > "0") {
        this.css += '  text-shadow: ' + textShadowH + ' ' + textShadowV + ' ' + textShadowB + ' ' + textShadowColor + ';\n';
    }
    if (document.getElementById("box-h-length").innerHTML > "0" || document.getElementById("box-v-length").innerHTML > "0" || document.getElementById("box-b-length").innerHTML > "0") {
        this.css += '  box-shadow: ' + boxShadowH + ' ' + boxShadowV + ' ' + boxShadowB + ' ' + boxShadowColor + ';\n';
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