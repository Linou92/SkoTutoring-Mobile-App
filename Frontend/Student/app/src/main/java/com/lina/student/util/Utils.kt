package com.lina.student.util

import android.app.Activity
import android.content.Context
import android.content.ContextWrapper
import android.graphics.Bitmap
import android.net.Uri
import android.provider.MediaStore
import androidx.fragment.app.DialogFragment
import android.util.DisplayMetrics
import android.view.inputmethod.InputMethodManager

import java.io.File
import java.io.FileOutputStream
import java.io.IOException
import java.text.DateFormat
import java.text.ParseException
import java.text.SimpleDateFormat
import java.util.*
import java.util.regex.Pattern


class Utils {
    companion object {

        val VALID_EMAIL_ADDRESS_REGEX = Pattern.compile("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,6}$", Pattern.CASE_INSENSITIVE)

        fun validateEmail(emailStr: String): Boolean {
            val matcher = VALID_EMAIL_ADDRESS_REGEX.matcher(emailStr)
            return matcher.find()
        }

        fun convertPixelsToDp(px: Float, context: Context): Float {
            val displayMetrics = context.resources.displayMetrics
            val dp = Math.round(px / (displayMetrics.xdpi / DisplayMetrics.DENSITY_DEFAULT))
            return dp.toFloat()
        }

        fun convertDpToPixel(dp: Float, context: Context): Float {
            val resources = context.resources
            val metrics = resources.displayMetrics
            return dp * (metrics.densityDpi.toFloat() / DisplayMetrics.DENSITY_DEFAULT)
        }

        fun stringIsNotEmptyOrNull(str: String?): Boolean {
            return if (str == null || str == "" || str.equals("null", ignoreCase = true) || str.equals("nul", ignoreCase = true)) false else true
        }

        fun getScreenHeightInDp(context: Activity): Float {
            val displayMetrics = DisplayMetrics()
            context.windowManager.defaultDisplay.getMetrics(displayMetrics)
            return displayMetrics.heightPixels.toFloat()
        }

        fun getScreenHeight(context: Activity): Float {
            val displayMetrics = DisplayMetrics()
            context.windowManager.defaultDisplay.getMetrics(displayMetrics)
            return displayMetrics.heightPixels.toFloat()
        }

        fun getScreenWidth(context: Activity): Float {
            val displayMetrics = DisplayMetrics()
            context.windowManager.defaultDisplay.getMetrics(displayMetrics)
            val width = displayMetrics.widthPixels
            return width.toFloat()
        }

        fun hideSoftKeyboard(activity: Activity) {
            try {
                val inputMethodManager = activity.getSystemService(Activity.INPUT_METHOD_SERVICE) as InputMethodManager
                inputMethodManager.hideSoftInputFromWindow(activity.currentFocus!!.windowToken, 0)
            } catch (e: Exception) {
            }

        }

        fun hideSoftKeyboardFromDialog(dialogFragment: DialogFragment) {
            try {
                val inputMethodManager = dialogFragment.activity!!.getSystemService(Activity.INPUT_METHOD_SERVICE) as InputMethodManager
                inputMethodManager.hideSoftInputFromWindow(dialogFragment.dialog?.currentFocus!!.windowToken, 0)
            } catch (e: Exception) {
            }

        }

        fun saveToInternalStorage(bitmapImage: Bitmap, name: String, context: Context): File {
            val cw = ContextWrapper(context)
            // path to /data/data/yourapp/app_data/imageDir
            val directory = cw.getDir("imageDir", Context.MODE_PRIVATE)
            // Create imageDir
            val mypath = File(directory, name)

            var fos: FileOutputStream? = null
            try {
                fos = FileOutputStream(mypath)
                // Use the compress method on the BitMap object to write image to the OutputStream
                bitmapImage.compress(Bitmap.CompressFormat.PNG, 100, fos)
            } catch (e: Exception) {
                e.printStackTrace()
            } finally {
                try {
                    fos!!.close()
                } catch (e: IOException) {
                    e.printStackTrace()
                }

            }
            return mypath
        }

        fun loadImageFromStorage(context: Context, name: String): File? {

            try {
                val cw = ContextWrapper(context)
                val directory = cw.getDir("imageDir", Context.MODE_PRIVATE)
                if (!directory.exists())
                    directory.mkdir()
                if (directory.length() / (1024 * 1024) > 50) {
                    directory.deleteOnExit()
                    return null
                }
                val d = directory.absolutePath
                val f = File("$directory/$name")
                if (f.exists()) {
                    return f
                }
                //            return BitmapFactory.decodeStream(new FileInputStream(f));
            } catch (e: Exception) {
                e.printStackTrace()
            }

            return null

        }

        fun getStorageDir(context: Context): String {

            try {
                val cw = ContextWrapper(context)
                val directory = cw.getDir("imageDir", Context.MODE_PRIVATE)
                if (!directory.exists())
                    directory.mkdir()
                return directory.absolutePath
                //            return BitmapFactory.decodeStream(new FileInputStream(f));
            } catch (e: Exception) {
                e.printStackTrace()
            }

            return ""

        }

        fun getCurrentDate(): Date {
            return Calendar.getInstance().time
        }

        fun getRealPathFromURI(contentURI: Uri, activity: Activity): String {
            val result: String
            val cursor = activity.contentResolver.query(contentURI, null, null, null, null)
            if (cursor == null) { // Source is Dropbox or other similar local file path
                result = contentURI.getPath()!!
            } else {
                cursor.moveToFirst()
                val idx = cursor.getColumnIndex(MediaStore.Images.ImageColumns.DATA)
                result = cursor.getString(idx)
                cursor.close()
            }
            return result
        }

        fun formatDate(date: Date): String {
            return DateFormat.getDateInstance().format(date)
        }

        fun getDateFromString(strDate: String): Date {
            var date: Date = Date()
            val format = SimpleDateFormat("yyyy-MM-dd", Locale.UK)
            try {
                date = format.parse(strDate)
            } catch (e: ParseException) {
                e.printStackTrace()
            }

            return date
        }

        fun getDateFromStringNull(strDate: String): Date? {
            var date: Date? = null
            val format = SimpleDateFormat("yyyy-MM-dd", Locale.UK)
            try {
                date = format.parse(strDate)
            } catch (e: ParseException) {
                e.printStackTrace()
            }

            return date
        }

    }

}
