/*Author:汪奇志 2010-12-10 首页滑动广告*/
var slider = {
    timer: null, cIndex: 0, count: 0, data: [], $obj: null, fxIsDone: true,
    //data:[{src:'',lnk:'',title:''}]
    init: function(data, elementId) {
        this.data = data;
        if (this.data.length < 1) return;
        this.count = this.data.length;
        var self = this;
        this.$obj = $("#" + elementId);
        var s = [];
        s.push('<a href="' + ($.trim(this.data[0].lnk) == "" ? "javascript:void(0)" : $.trim(this.data[0].lnk)) + '" id="foclnk" target="_blank"><img id="focpic" style="FILTER: RevealTrans(duration=1;ransition=12); VISIBILITY: visible; POSITION: absolute" src="' + this.data[0].src + '" /></a>');
        s.push('<div class="thumb_title" id="foctitle"><a href="javascript:void(0)">' + this.data[0].title + '</a></div>');
        s.push('<div id="Slide_Thumb">');
        s.push('<ul>');

        for (var i = 0; i < this.data.length; i++) {
            s.push('<li class="' + (i == 0 ? 'thumb_on' : 'thumb_off') + '"><div><a href="javascript:void(0)"><img src="' + this.data[i].src + '"/></a></div></li>');
        }

        s.push('</ul>');
        s.push('</div>');

        this.$obj.html(s.join(''));

        this.$obj.find('ul>li').each(function(i) {
            $(this).mouseover(function() {
                self.setFocus(i);
            }).mouseout(function() {
                self.play();
            });
        })

        this.play();
    },
    setFocus: function(index) {
        //if (index == this.cIndex) return;
        var $focusPic = $("#focpic");
        var $focusLnk = $("#foclnk");
        this.cIndex = index;

        $focusPic.attr("src", this.data[index].src);
        $focusLnk.attr("href", $.trim(this.data[index].lnk) == "" ? "javascript:void(0)" : $.trim(this.data[index].lnk));

        this.$obj.find("ul>li").removeClass("thumb_on").addClass("thumb_off");
        this.$obj.find("ul>li").eq(index).addClass("thumb_on");

        try {
            $focusPic[0].style.visibility = "hidden";
            $focusPic[0].filters[0].Apply();
            if ($focusPic[0].style.visibility == "visible") {
                $focusPic[0].style.visibility = "hidden";
                $focusPic[0].filters.revealTrans.transition = 23;
            }
            else {
                $focusPic[0].style.visibility = "visible";
                $focusPic[0].filters[0].transition = 23;
            }
            $focusPic[0].filters[0].Play();
        } catch (e) {
            var self = this;
            $focusPic[0].style.visibility = "visible";
            
            if (self.fxIsDone) {
                self.fxIsDone = false;
                $focusPic.fadeTo("fast", 0.1, function() {
                    $focusPic.fadeTo("slow", 0.9, function() { self.fxIsDone = true; });
                });
            }            
        }

        this.stop();
    },
    play: function() {
        var self = this;
        this.timer = setTimeout(function() { self.playNext(); }, 4500);
    },
    playNext: function() {
        if (this.cIndex == this.count - 1) {
            this.cIndex = 0;
        }
        else {
            this.cIndex++;
        };
        this.setFocus(this.cIndex);
        this.play();
    },
    stop: function() {
        clearTimeout(this.timer);
    }
};

