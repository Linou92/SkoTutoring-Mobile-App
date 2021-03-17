package com.lina.student.ui.extension

import android.graphics.PorterDuff
import android.net.Uri
import androidx.core.content.ContextCompat
import android.widget.ImageView
import androidx.swiperefreshlayout.widget.CircularProgressDrawable
import com.bumptech.glide.Glide
import com.bumptech.glide.request.RequestOptions
import com.lina.student.R
import java.io.File

/**
 * Created by tahadouaji on 3/27/19.
 */

fun ImageView.loadUrl(url: String) {
    Glide.with(context)
            .load(url)
            .into(this);
}
fun ImageView.loadUrl_(url:String){
    val circularProgressDrawable = CircularProgressDrawable(context)
    circularProgressDrawable.strokeWidth = 10f
    circularProgressDrawable.centerRadius = 30f
    circularProgressDrawable.setColorFilter(ContextCompat.getColor(context, R.color.app_blue), PorterDuff.Mode.SRC_IN )
    circularProgressDrawable.start()
    if( !(url=="" || url==null)){
        Glide.with(this.context)
                .load(url)
                .apply(RequestOptions()
                        .placeholder(circularProgressDrawable)
                        .error(R.drawable.ic_broken_image))
                .into(this)
    }


}
fun ImageView.loadUrl(url: String, drawableId: Int) {
    Glide.with(context)
            .load(url)
            .placeholder(drawableId)
            .into(this)
}

fun ImageView.loadUrl(url: String, drawableId: Int, width: Int, height: Int) {
    Glide.with(context)
            .load(url)
            .override(width, height)
            .placeholder(drawableId)
            .into(this)
}

fun ImageView.loadUrlCircle(url: String, drawableId: Int) {
    Glide.with(context)
            .load(url)
            .placeholder(drawableId)
            .apply(RequestOptions.circleCropTransform())
            .into(this)
}

fun ImageView.loadFile(file: File) {
    Glide.with(context)
            .load(file)
            .into(this)
}

fun ImageView.loadFile(file: Uri) {
    Glide.with(context)
            .load(file)
            .into(this)
}

fun ImageView.loadResource(drawableId: Int) {
    this.setImageDrawable(ContextCompat.getDrawable(context, drawableId))
}


fun ImageView.toggleImage(selected: Boolean, selectedDrawable: Int, unselectedDrawable: Int) {
    if (selected) {
        this.loadResource(selectedDrawable)
    } else {
        this.loadResource(unselectedDrawable)
    }
}