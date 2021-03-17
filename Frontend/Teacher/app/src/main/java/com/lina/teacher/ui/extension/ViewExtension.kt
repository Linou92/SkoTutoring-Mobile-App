package com.lina.teacher.ui.extension

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup



fun View.visible() {
    visibility = View.VISIBLE
}

fun View.inVisible() {
    visibility = View.INVISIBLE
}

fun View.inVisibleWithDisable() {
    disable()
    visibility = View.INVISIBLE
}

fun View.visibleWithEnable() {
    enable()
    visibility = View.VISIBLE
}


fun View.gone() {
    visibility = View.GONE
}


fun View.disable() {
    alpha = 0.7f
    isEnabled = false
}

fun View.enable() {
    alpha = 1f
    isEnabled = true
}

fun ViewGroup.inflate(layoutRes: Int): View {
    return LayoutInflater.from(context).inflate(layoutRes, this, false)
}