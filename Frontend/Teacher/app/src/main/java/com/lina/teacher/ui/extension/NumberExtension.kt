package com.lina.teacher.ui.extension



fun Int.toStringNotZero(): String {
    if (this > 0)
        return this.toString()
    return ""
}

fun Double.toStringNotZero(): String {
    if (this > 0)
        return this.toString()
    return ""
}

fun Long.toStringNotZero(): String {
    if (this > 0)
        return this.toString()
    return ""
}

fun Float.toStringNotZero(): String {
    if (this > 0)
        return this.toString()
    return ""
}