package com.lina.student.ui

import android.os.Bundle
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.DialogFragment
import com.lina.student.util.Utils


open class TDDialogFragment : DialogFragment() {

    var progressBar: View? = null
    var reloadView: View? = null
    var containerView: View? = null

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        if (view is ViewGroup) {
        }

    }

    fun showProgressView(value: Boolean) {
        progressBar?.visibility = if (value) View.VISIBLE else View.INVISIBLE
        containerView?.visibility = if (value) View.INVISIBLE else View.VISIBLE
    }

    fun showReloadView(value: Boolean) {
        reloadView?.visibility = if (value) View.VISIBLE else View.GONE
        progressBar?.visibility = if (value) View.GONE else View.VISIBLE
    }

    fun hideAllRequestViews() {
        progressBar?.visibility = View.GONE
        reloadView?.visibility = View.GONE
    }

    fun showToast(msg: String) {
        activity?.let {
            Toast.makeText(it, msg, Toast.LENGTH_SHORT).show()
        }
    }

    fun hideKeyboard() {
        activity?.let {
            Utils.hideSoftKeyboard(it)
        }
    }
}