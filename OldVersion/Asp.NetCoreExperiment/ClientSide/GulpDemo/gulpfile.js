/// <binding BeforeBuild='default' Clean='clean, default' />
"use strict";

const gulp = require("gulp"),            //Gulp 流式处理生成系统
    rimraf = require("rimraf"),          //节点删除模块
    concat = require("gulp-concat"),     //连接基于操作系统的换行字符的文件的模块
    cssmin = require("gulp-cssmin"),     //缩减 CSS 文件的模块
    uglify = require("gulp-uglify");     //缩减 JS 文件的模块

const paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", done => rimraf(paths.concatJsDest, done));     //一个使用 rimraf Node 删除模块删除缩减版 site.js 文件的任务
gulp.task("clean:css", done => rimraf(paths.concatCssDest, done));   //一个使用 rimraf Node 删除模块删除缩减版 site.css 文件的的任务
gulp.task("clean", gulp.series(["clean:js", "clean:css"]));          //调用的任务clean:js任务，紧跟clean:css任务。

gulp.task("min:js", () => {                                          //缩减并将连接的 js 文件夹中的所有.js 文件的任务，但min.js 文件除外
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", () => {                                        //缩减并将连接的 css 文件夹中的所有.css 文件的任务，但min.css 文件除外
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", gulp.series(["min:js", "min:css"]));               //调用的任务min:js任务紧跟min:css任务

// A 'default' task is required by Gulp v4
gulp.task("default", gulp.series(["min"]));


gulp.task('first', done => {
    console.log('first task! <-----');
    done(); // signal completion
});