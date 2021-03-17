package com.lina.teacher.helper

import android.content.Context
import com.qingmei2.rximagepicker.entity.sources.Camera
import com.qingmei2.rximagepicker.entity.sources.Gallery
import com.qingmei2.rximagepicker.entity.Result
import io.reactivex.Observable


interface IImagePicker {

    @Gallery    // open gallery
    fun openGallery(context: Context): Observable<Result>

    @Camera     // take photos
    fun openCamera(context: Context): Observable<Result>
}
