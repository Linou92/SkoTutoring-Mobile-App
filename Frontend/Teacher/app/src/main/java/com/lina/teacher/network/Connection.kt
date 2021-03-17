package com.lina.teacher.network


import com.androidnetworking.error.ANError
import org.json.JSONObject
import com.androidnetworking.AndroidNetworking
import android.graphics.Bitmap
import com.androidnetworking.interfaces.BitmapRequestListener
import android.util.Log
import com.androidnetworking.common.Priority
import com.lina.teacher.R
import com.lina.teacher.TemplateApplication
import com.lina.teacher.helper.JsonHelper
import com.rx2androidnetworking.Rx2AndroidNetworking
import java.io.File
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.Disposable
import io.reactivex.schedulers.Schedulers

class Connection {
    companion object {


        fun get(url: String, tag: String, subscriber: io.reactivex.Observer<JSONObject>) {
            Log.i("AndroidNetworking", "get $tag")

            try {
                Rx2AndroidNetworking.get(url)
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .jsonObjectObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<JSONObject> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onNext(t: JSONObject) {
                            Log.i(
                                "AndroidNetworking",
                                "get " + tag + " " + JsonHelper.objectToString(t)
                            )
                            subscriber.onNext(t)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "get " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                    })
            } catch (e: Exception) {
            }

        }

        fun getStr(
            url: String,
            params: Any,
            headerAuthorization: String,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Log.i("AndroidNetworking", "get $url")

            try {
                Rx2AndroidNetworking.get(url)
                    .addQueryParameter(params)
                    .addHeaders("Content-Type", "application/json")
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .stringObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<String> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "get " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                        override fun onNext(t: String) {
                            subscriber.onNext(JSONObject(t))
                        }

                    })
            } catch (e: Exception) {
                val error = Throwable(
                    TemplateApplication.getApplicationInstance()
                        .getString(R.string.connection_error_message), null
                )
                subscriber.onError(error)
            }

        }

        fun get(
            url: String,
            params: Any,
            headerAuthorization: String,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Log.i("AndroidNetworking", "get $url")

            try {
                Rx2AndroidNetworking.get(url)
                    .addQueryParameter(params)
                    .addHeaders("Content-Type", "application/json")
                    .addHeaders("Authorization", "Bearer $headerAuthorization")
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .jsonObjectObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<JSONObject> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onNext(t: JSONObject) {
                            Log.i(
                                "AndroidNetworking",
                                "get " + tag + " " + JsonHelper.objectToString(t)
                            )
                            subscriber.onNext(t)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "get " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                    })
            } catch (e: Exception) {
                val error = Throwable(
                    TemplateApplication.getApplicationInstance()
                        .getString(R.string.connection_error_message), null
                )
                subscriber.onError(error)
            }

        }

        fun post(
            url: String,
            params: Any,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Log.i("AndroidNetworking", "post " + tag + " " + JsonHelper.objectToString(params))

            try {
                Rx2AndroidNetworking.post(url)
                    .addJSONObjectBody(params as JSONObject?)
                    .addHeaders("Content-Type", "application/json")
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .jsonObjectObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<JSONObject> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onNext(t: JSONObject) {
                            Log.i(
                                "AndroidNetworking",
                                "post " + tag + " " + JsonHelper.objectToString(t)
                            )
                            subscriber.onNext(t)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "post " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                    })
            } catch (e: Exception) {
            }


        }

        fun post(
            url: String,
            params: Any,
            headerAuthorization: String,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Log.i("AndroidNetworking", "post " + tag + " " + JsonHelper.objectToString(params))

            try {
                Rx2AndroidNetworking.post(url)
                    .addJSONObjectBody(params as JSONObject)
                    .addHeaders("Content-Type", "application/json")
                    .addHeaders("Authorization", "Bearer $headerAuthorization")
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .jsonObjectObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<JSONObject> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onNext(t: JSONObject) {
                            Log.i(
                                "AndroidNetworking",
                                "post " + tag + " " + JsonHelper.objectToString(t)
                            )
                            subscriber.onNext(t)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "post " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                    })
            } catch (e: Exception) {
                val error = Throwable(
                    TemplateApplication.getApplicationInstance()
                        .getString(R.string.connection_error_message), null
                )
                subscriber.onError(error)
            }


        }

        fun put(
            url: String,
            params: Any,
            headerAuthorization: String,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Log.i("AndroidNetworking", "post " + tag + " " + JsonHelper.objectToString(params))

            try {
                Rx2AndroidNetworking.put(url)
                    .addJSONObjectBody(params as JSONObject?)
                    .addHeaders("Content-Type", "application/json")
                    .addHeaders("Authorization", "Bearer $headerAuthorization")
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .jsonObjectObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<JSONObject> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onNext(t: JSONObject) {
                            Log.i(
                                "AndroidNetworking",
                                "post " + tag + " " + JsonHelper.objectToString(t)
                            )
                            subscriber.onNext(t)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "post " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                    })
            } catch (e: Exception) {
                val error = Throwable(
                    TemplateApplication.getApplicationInstance()
                        .getString(R.string.connection_error_message), null
                )
                subscriber.onError(error)
            }


        }

        fun delete(
            url: String,
            params: Any,
            headerAuthorization: String,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Log.i("AndroidNetworking", "post " + tag + " " + JsonHelper.objectToString(params))

            try {
                Rx2AndroidNetworking.delete(url)
                    .addJSONObjectBody(params as JSONObject?)
                    .addHeaders("Content-Type", "application/json")
                    .addHeaders("Authorization", "Bearer $headerAuthorization")
                    .setTag(tag)
                    .setPriority(Priority.MEDIUM)
                    .build()
                    .jsonObjectObservable
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(object : io.reactivex.Observer<JSONObject> {
                        override fun onComplete() {
                            subscriber.onComplete()
                        }

                        override fun onSubscribe(d: Disposable) {
                            subscriber.onSubscribe(d)
                        }

                        override fun onNext(t: JSONObject) {
                            Log.i(
                                "AndroidNetworking",
                                "post " + tag + " " + JsonHelper.objectToString(t)
                            )
                            subscriber.onNext(t)
                        }

                        override fun onError(e: Throwable) {
                            Log.i("AndroidNetworking", "post " + tag + " " + e.message)
                            subscriber.onError(e)
                        }

                    })
            } catch (e: Exception) {
                val error = Throwable(
                    TemplateApplication.getApplicationInstance()
                        .getString(R.string.connection_error_message), null
                )
                subscriber.onError(error)
            }


        }

        fun postImage(
            url: String,
            params: Any,
            file: File,
            fileKey: String,
            headerAuthorization: String,
            tag: String,
            subscriber: io.reactivex.Observer<JSONObject>
        ) {
            Rx2AndroidNetworking.upload(url)
                .addHeaders("Content-Type", "application/json")
                .addHeaders("Authorization", "Bearer $headerAuthorization")
                .addMultipartParameter(params)
                .addMultipartFile(fileKey, file)
                .setTag(tag)
                .setPriority(Priority.MEDIUM)
                .build().jsonObjectObservable
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(object : io.reactivex.Observer<JSONObject> {
                    override fun onComplete() {
                        subscriber.onComplete()
                    }

                    override fun onSubscribe(d: Disposable) {
                        subscriber.onSubscribe(d)
                    }

                    override fun onNext(t: JSONObject) {
                        Log.i(
                            "AndroidNetworking",
                            "post " + tag + " " + JsonHelper.objectToString(t)
                        )
                        subscriber.onNext(t)
                    }

                    override fun onError(e: Throwable) {
                        Log.i("AndroidNetworking", "post " + tag + " " + e.message)
                        subscriber.onError(e)
                    }

                })
        }

        fun getBitmap(url: String, width: Int, height: Int, tag: String) {
            AndroidNetworking.get(url)
                .setTag(tag)
                .setPriority(Priority.MEDIUM)
                .setBitmapMaxHeight(height)
                .setBitmapMaxWidth(width)
                .setBitmapConfig(Bitmap.Config.ARGB_8888)
                .build()
                .getAsBitmap(object : BitmapRequestListener {
                    override fun onResponse(bitmap: Bitmap) {

                    }

                    override fun onError(error: ANError) {

                    }
                })
        }

        fun cancel(tag: String) {
            AndroidNetworking.cancel(true)
        }

    }
}