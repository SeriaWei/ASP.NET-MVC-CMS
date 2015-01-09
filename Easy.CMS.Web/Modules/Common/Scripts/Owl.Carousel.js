$(document).ready(function () {
            var time = 4;
            var $progressBar,
              $bar,
              $elem,
              isPause,
              tick,
              percentTime;
            $('.owl-carousel').owlCarousel({
                slideSpeed: 500,
                paginationSpeed: 500,
                singleItem: true,
                navigation: true,
                navigationText: [
                "<i class='fa fa-angle-left'></i>",
                "<i class='fa fa-angle-right'></i>"
                ],
                afterInit: progressBar,
                afterMove: moved,
                startDragging: pauseOnDragging,
                //autoHeight : true,
                transitionStyle: "fadeUp"
            });

            //Init progressBar where elem is $("#owl-demo")
            function progressBar(elem) {
                $elem = elem;
                //build progress bar elements
                buildProgressBar();
                //start counting
                start();
            }

            function buildProgressBar() {
                $progressBar = $("<div class='progressBar'>");
                $bar = $("<div class='bar'>");
                $progressBar.append($bar).appendTo($elem);
            }

            function start() {
                percentTime = 0;
                isPause = false;
                tick = setInterval(interval, 10);
            };

            function interval() {
                if (isPause === false) {
                    percentTime += 1 / time;
                    $bar.css({
                        width: percentTime + "%"
                    });
                    if (percentTime >= 100) {
                        $elem.trigger('owl.next')
                    }
                }
            }
            function pauseOnDragging() {
                isPause = true;
            }
            function moved() {
                clearTimeout(tick);
                start();
            }
        });